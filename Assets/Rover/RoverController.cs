using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;
using System.Diagnostics;


public class RoverController : MonoBehaviour
{
    // external objects assigned from inspector
    public GameObject scoreboard;
    public AudioSource collectSFX;
    public float grabDistance = 1f;
    public LayerMask boulderMask;
    public SpriteMask flashlight;

    // rover objects and settings
    private Rigidbody2D rover;
    private float speed_init;
    private float speed;
    private float boost;
    private Animator animator;
    private Vector2 input;
    private int roverLevel;
    private ScoreBoard score;
    private string animationLevel;
    private float horizontal;
    private float vertical;
    private float angle;

    //pop up related objects and variables
    public GameObject popUpPanel;
    bool ironIsNew = true;
    bool sapphireIsNew = true;
    bool tungstenIsNew = true;
    bool aluminumIsNew = true;
    bool sulfurIsNew = true;

    public GameObject upgradePopUp;
    bool boostUnlocked = false;
    bool transmitterUnlocked = false;
    bool flashlightUnlocked = false;
    bool grabberUnlocked = false;

    //score progress objects and variables
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI scoreNeededText;
    public Image fill;
    public int scoreNeeded = 10; //initialized for first upgrade (boost)

    //upgrades in pause menu objects and variables
    public TextMeshProUGUI nextUpgradeText;

    // interactable objects
    private RaycastHit2D hit;
    private GameObject boulder;
    private float horizontalLock;
    private float verticalLock;
    private bool isPulling;

    //creates an instance of the player rover
    private static RoverController roverInstance = null;

    private void Awake()
    {
        //rover duplication management
        if (roverInstance == null) //the first time main world loads
        {
            roverInstance = this; //set rover instance
            DontDestroyOnLoad(this.gameObject); //used to keep the same rover object in all scenes
            return;
        }
        Destroy(this.gameObject); //destroy duplicate rover objects when returning to MainWorld
    }

    // Start is called before the first frame update
    void Start()
    {
        // init objects
        rover = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        score = scoreboard.GetComponent<ScoreBoard>();

        // init settings
        speed_init = 150.0f;
        speed = speed_init;
        boost = speed_init * 5;
        roverLevel = 1;
        animationLevel = "L1_";
        isPulling = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (roverLevel == 1 && score.getScore() >= 6)
        {
            roverLevel = 2;
            animationLevel = "L2_";
        }

        if (roverLevel == 2 && score.getScore() >= 9)
        {
            roverLevel = 3;
            animationLevel = "L3_";

        }

        if (roverLevel == 3 && score.getScore() >= 12)
        {
            roverLevel = 4;
            animationLevel = "L4_";

        }

        if (roverLevel == 4 && score.getScore() >= 15)
        {
            roverLevel = 5;
            animationLevel = "L5_";

        }


        if (!PauseMenu.isPaused)
        {
            if (!PopUp.popUpElementActive)
            {
                if (!PopUp.popUpUpgradeActive)
                {
                    Move();
                }
            }
        }
    }

    /*
     * Move
     *  Defines movement characteristics for the rover.
     */
    private void Move()
    {

        // recieve horizontal and veritcal input from user - wasd or arrows
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
                             
        // set raycast for finding pullable objects in the direction rover is going
        Physics2D.queriesStartInColliders = false;
        hit = Physics2D.Raycast(transform.position, input, grabDistance, boulderMask);

        // grab boulder on shift down
        if (roverLevel >= 5 && hit.collider != null && hit.collider.gameObject.tag == "Pullable")
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) 
            {
                // attach boulder RB to rover RB
                boulder = hit.collider.gameObject;
                boulder.GetComponent<FixedJoint2D>().enabled = true;
                boulder.GetComponent<FixedJoint2D>().connectedBody = rover;

                // lock in current direction and set isPulling
                horizontalLock = animator.GetFloat("MovementX");
                verticalLock = animator.GetFloat("MovementY");
                isPulling = true;
            }
            
        }

        // release boulder on shift up
        if (boulder != null && Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            // detach boulder RB from rover RB
            boulder.GetComponent<FixedJoint2D>().connectedBody = null;
            boulder.GetComponent<FixedJoint2D>().enabled = false;
            boulder = null;

            isPulling = false;
        }

        // without motion input, idle the rover without changing direction
        if (horizontal == 0 && vertical == 0)
        {
            animator.Play(animationLevel + "idle");
            rover.velocity = Vector2.zero;
            return;
        }

        // set 'input' to what the user entered
        input = new Vector2(horizontal, vertical);

        // set rover direction in the animator and move it on screen
        // if pulling -> lock direction, else -> regular direction
        if (isPulling)
        {
            animator.SetFloat("MovementX", horizontalLock);
            animator.SetFloat("MovementY", verticalLock);

            // limit to regular speed while pulling
            speed = speed_init;
            animator.Play(animationLevel + "move");

        }
        else
        {
            animator.SetFloat("MovementX", input.x);
            animator.SetFloat("MovementY", input.y);

            

            // move rover with or without boost
            if (roverLevel > 1 && Input.GetKey(KeyCode.Space))
            {
                speed = boost;
                animator.Play(animationLevel + "boost");
            }
            else
            {
                speed = speed_init;
                animator.Play(animationLevel + "move");
            }
        }
        rover.velocity = input * speed * Time.fixedDeltaTime;

        // move flashlight mask
        angle = Vector2.Angle(Vector2.up, input);
        flashlight.transform.Rotate(angle, 0, 0, Space.Self);
        UnityEngine.Debug.Log(angle);


    }

    // this is to draw the raycast to the "Scene" view and not show it in the "Game" view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + input * grabDistance);
    }



    /*
     * OnCollisionStay2D
     *  Defines behavior while in contact. 
     *  Checks for element based on tag, 'collects' element on E press, updates score.
     */
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Element")
        {
            if (Input.GetKey(KeyCode.E))
            {

                //int val = element.GetComponent<GenericElement>().getElement();
                int val = collision.gameObject.GetComponent<GenericElement>().getElement();
                collectSFX.Play();
                score.adjustScore(val);

                //update score progress in pause menu
                currentScoreText.text = $"Current Score is {score.getScore()}";

                if (score.getScore() < 40) // maximum upgrade score
                {
                    scoreNeededText.text = $"{scoreNeeded - score.getScore()} Point(s) Needed to Upgrade!";
                    fill.fillAmount = (float)score.getScore() / scoreNeeded;
                }

                //new upgrade pop up + pause menu
                if (score.getScore() >= 10 && !boostUnlocked) //boost unlocked when score is 10
                {  

                    //pop up
                    PopUp.popUpUpgradeActive = true; //pauses controls
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("BoostInfo").gameObject.SetActive(true); //boost related info on pop up appears 
                    boostUnlocked = true;

                    //change progress bar goal for next upgrade
                    scoreNeeded = 20;
                    scoreNeededText.text = $"{scoreNeeded - score.getScore()} Point(s) Needed to Upgrade!";
                    fill.fillAmount = (float)score.getScore() / scoreNeeded;

                    //update upgrades in pause menu
                    nextUpgradeText.text = "Transmitter"; //change to next upgrade
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("BoostInfo").gameObject.SetActive(true);

                }
                if (score.getScore() >= 20 && !transmitterUnlocked) //transmitter unlocked when score is 20***
                {
                    //pop up
                    PopUp.popUpUpgradeActive = true; //pauses controls
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("TransmitterInfo").gameObject.SetActive(true); //transmitter related info on pop up appears 
                    transmitterUnlocked = true;

                    //change progress bar goal for next upgrade
                    scoreNeeded = 30;
                    scoreNeededText.text = $"{scoreNeeded - score.getScore()} Point(s) Needed to Upgrade!";
                    fill.fillAmount = (float)score.getScore() / scoreNeeded;

                    //update upgrades in pause menu
                    nextUpgradeText.text = "Flashlight"; //change to whatever next upgrade is
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("TransmitterInfo").gameObject.SetActive(true);
                }
                if (score.getScore() >= 30 && !flashlightUnlocked) //flashlight unlocked when score is 30***
                {
                    //pop up
                    PopUp.popUpUpgradeActive = true; //pauses controls
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("FlashlightInfo").gameObject.SetActive(true); //flashlight related info on pop up appears 
                    flashlightUnlocked = true;

                    //change progress bar goal for next upgrade
                    scoreNeeded = 40;
                    scoreNeededText.text = $"{scoreNeeded - score.getScore()} Point(s) Needed to Upgrade!";
                    fill.fillAmount = (float)score.getScore() / scoreNeeded;

                    //update upgrades in pause menu
                    nextUpgradeText.text = "Grabber Claw"; //change to whatever next upgrade is
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("FlashlightInfo").gameObject.SetActive(true);
                }
                if (score.getScore() >= 40 && !grabberUnlocked) //grabber claw unlocked when score is 40***
                {
                    //pop up
                    PopUp.popUpUpgradeActive = true; //pauses controls
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("GrabberInfo").gameObject.SetActive(true); //grabber claw related info on pop up appears 
                    grabberUnlocked = true;

                    //change progress bar goal for next upgrade
                    scoreNeeded = 0;
                    scoreNeededText.text = $"All upgrades complete!";
                    scoreNeededText.transform.position = new Vector3(525f, 230f, 0f);
                    fill.fillAmount = 100;

                    //update upgrades in pause menu
                    nextUpgradeText.text = "All upgrades complete!"; //change to whatever next upgrade is
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("GrabberInfo").gameObject.SetActive(true);
                }

                //new element pop ups + pause menu
                switch (val) //switch determines which element was just picked up
                {
                    case 3: // iron was picked up
                        {
                            if (ironIsNew) //pop ups only appear the first time an element is picked up
                            {
                                PopUp.popUpElementActive = true; //pauses controls
                                popUpPanel.gameObject.SetActive(true); //pop up appears
                                popUpPanel.transform.Find("NewElementText").gameObject.SetActive(true); //new element text appears
                                popUpPanel.transform.Find("IronInfo").gameObject.SetActive(true); //iron info on pop up appears 

                                //add iron info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_iron").Find("IronButton").gameObject.SetActive(true);

                                ironIsNew = false;
                            }
                            break;
                        }

                    case 5: // tungsten was picked up
                        {
                            if (tungstenIsNew)
                            {
                                PopUp.popUpElementActive = true; //pauses controls
                                popUpPanel.gameObject.SetActive(true); //pop up appears
                                popUpPanel.transform.Find("NewElementText").gameObject.SetActive(true); //new element text appears
                                popUpPanel.transform.Find("TungstenInfo").gameObject.SetActive(true); //tungsten info on pop up appears 

                                //add tungsten info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_tungsten").Find("TungstenButton").gameObject.SetActive(true);

                                tungstenIsNew = false;
                            }
                            break;
                        }

                    case 10: // sapphire was picked up
                        {
                            if (sapphireIsNew)
                            {
                                PopUp.popUpElementActive = true; //pauses controls
                                popUpPanel.gameObject.SetActive(true); //pop up appears
                                popUpPanel.transform.Find("NewElementText").gameObject.SetActive(true); //new element text appears
                                popUpPanel.transform.Find("SapphireInfo").gameObject.SetActive(true); //sapphire info on pop up appears 

                                //add sapphire info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_sapphire").Find("SapphireButton").gameObject.SetActive(true);

                                sapphireIsNew = false;
                            }
                            break;
                        }

                    case 0: // aluminum was picked up   **********update value**********
                        {
                            if (aluminumIsNew)
                            {
                                PopUp.popUpElementActive = true; //pauses controls
                                popUpPanel.gameObject.SetActive(true); //pop up appears
                                popUpPanel.transform.Find("NewElementText").gameObject.SetActive(true); //new element text appears
                                popUpPanel.transform.Find("AluminumInfo").gameObject.SetActive(true); //aluminum info on pop up appears 

                                //add aluminum info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_aluminum").Find("AluminumButton").gameObject.SetActive(true);

                                aluminumIsNew = false;
                            }
                            break;
                        }

                    case 1: // sulfur was picked up   **********update value**********
                        {
                            if (sulfurIsNew)
                            {
                                PopUp.popUpElementActive = true; //pauses controls
                                popUpPanel.gameObject.SetActive(true); //pop up appears
                                popUpPanel.transform.Find("NewElementText").gameObject.SetActive(true); //new element text appears
                                popUpPanel.transform.Find("SulfurInfo").gameObject.SetActive(true); //sulfur info on pop up appears 

                                //add sulfur info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_sulfur").Find("SulfurButton").gameObject.SetActive(true);

                                sulfurIsNew = false;
                            }
                            break;
                        }
                }

            }
        }

        /*
        if (collision.gameObject.tag == "Pullable" && Input.GetKey(KeyCode.LeftShift))
        {
            boulder = collision.gameObject;
            boulder.transform.SetParent(this.transform);
        }
        */
        
    }


}

