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
using System;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class RoverController : MonoBehaviour
{
    // external objects assigned from inspector
    public SpriteRenderer roverSprite;
    public GameObject scoreboard;
    public GameObject triangleRight;
    public GameObject triangleLeft;
    public GameObject triangleDown;
    public GameObject triangleUp;
    public GameObject triangleUpRight;
    public GameObject triangleUpLeft;
    public GameObject triangleDownLeft;
    public GameObject triangleDownRight;
    public GameObject journal;
    public AudioSource collectSFX;
    public float grabDistance = 1f;
    public LayerMask boulderMask;
    public SpriteMask flashlight;
    public int numberOfPages = 1;
    public GameObject NASABase;

    // rover objects and settings
    private Rigidbody2D rover;
    private float speed_init;
    private float speed;
    private float boost;
    private Animator animator;
    private Vector2 input;
    public int roverLevel;
    private ScoreBoard score;
    private string animationLevel;
    private float horizontal;
    private float vertical;
    private float angle;
    private int level2 = 100;
    private int level3 = 500;
    private int level4 = 1250;
    private int level5 = 7500;

    //pop up related objects and variables
    public GameObject popUpPanel;
    public bool ironIsNew = true;
    public bool rockIsNew = true;
    public bool micaIsNew = true;
    public bool nickelIsNew = true;
    public bool olivineIsNew = true;
    public bool feldsparIsNew = true;
    public bool pyroxeneIsNew = true;
    public bool quartzIsNew = true;

    public GameObject upgradePopUp;
    bool boostUnlocked = false;
    bool transmitterUnlocked = false;
    bool flashlightUnlocked = false;
    bool grabberUnlocked = false;

    //score progress objects and variables
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI scoreNeededText;
    public Image fill;
    public int scoreNeeded = 100; //initialized for first upgrade (boost)

    //upgrades in pause menu objects and variables
    public TextMeshProUGUI nextUpgradeText;

    // interactable objects
    private RaycastHit2D hit;
    private GameObject boulder;
    private float horizontalLock;
    private float verticalLock;
    private bool isPulling;

    //list of coordinates for collected elements + breakable obstacles - used to destroy collected object upon scene reload
    public List<Vector3> elementCoordinates = new List<Vector3>();
    public List<Vector3> obstacleCoordinates = new List<Vector3>();
    public List<Vector3> journalCoordinates = new List<Vector3>();

    //coordinates near cave entrance that setSpawn() will move rover to anytime hub world loads
    private Vector3 caveCoordinates = new Vector3(-95.97f, -9.04f, 0); //initialize to spawn point for startup of the game

    //coordinates near cave entrance that setSpawn() will move rover to anytime cave 4 loads
    private Vector3 cave4Coordinates = new Vector3(-1.46f, -47.84f, 0); //initialize to cave 4 entrance from hub world

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
        Destroy(this.gameObject); //destroy duplicate rover objects when returning to HubWorld
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
        Sprite rank = roverSprite.sprite;
        if (roverLevel >= 4)
        {
            if (rank.name.Contains("Right"))
            {
                triangleRight.SetActive(true);
            }
            else
            {
                triangleRight.SetActive(false);

            }
            if (rank.name.Contains("Left"))
            {
                triangleLeft.SetActive(true);
            }
            else
            {
                triangleLeft.SetActive(false);

            }
            if (rank.name.Contains("Down"))
            {
                triangleDown.SetActive(true);
            }
            else
            {
                triangleDown.SetActive(false);

            }
            if (rank.name.Contains("Up"))
            {
                triangleUp.SetActive(true);
            }
            else
            {
                triangleUp.SetActive(false);

            }
            if (rank.name.Contains("Up") && rank.name.Contains("Left"))
            {
                triangleUpLeft.SetActive(true);
                triangleUp.SetActive(false);
                triangleLeft.SetActive(false);
            }
            else
            {
                triangleUpLeft.SetActive(false);

            }
            if (rank.name.Contains("Up") && rank.name.Contains("Right"))
            {
                triangleUpRight.SetActive(true);
                triangleUp.SetActive(false);
                triangleRight.SetActive(false);
            }
            else
            {
                triangleUpRight.SetActive(false);

            }
            if (rank.name.Contains("Down") && rank.name.Contains("Left"))
            {
                triangleDownLeft.SetActive(true);
                triangleDown.SetActive(false);
                triangleLeft.SetActive(false);
            }
            else
            {
                triangleDownLeft.SetActive(false);

            }
            if (rank.name.Contains("Down") && rank.name.Contains("Right"))
            {
                triangleDownRight.SetActive(true);
                triangleDown.SetActive(false);
                triangleRight.SetActive(false);
            }
            else
            {
                triangleDownRight.SetActive(false);

            }
        }
        if (roverLevel == 1 && score.getScore() >= level2)
        {
            roverLevel = 2;
            animationLevel = "L2_";
        }

        if (roverLevel == 2 && score.getScore() >= level3)
        {
            roverLevel = 3;
            animationLevel = "L3_";

        }

        if (roverLevel == 3 && score.getScore() >= level4)
        {
            roverLevel = 4;
            animationLevel = "L4_";

        }

        if (roverLevel == 4 && score.getScore() >= level5)
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
                    if (!PopUp.popUpFactsActive)
                    {
                        Move();
                    }   
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
            animator.SetFloat("MovementX", horizontal);
            animator.SetFloat("MovementY", vertical);

            

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
        //angle = Vector2.Angle(Vector2.up, input);
        //flashlight.transform.Rotate(angle, 0, 0, Space.Self);
        //UnityEngine.Debug.Log(angle);


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

                //store coordinates of the collected element to list
                Vector3 coordinates = collision.gameObject.GetComponent<GenericElement>().getCoordinates();
                elementCoordinates.Add(coordinates);

                //update score progress in pause menu
                currentScoreText.text = $"Current Score is {score.getScore()}";

                if (score.getScore() < level5) // maximum upgrade score
                {
                    scoreNeededText.text = $"{scoreNeeded - score.getScore()} Points Needed to Upgrade!";
                    fill.fillAmount = (float)score.getScore() / scoreNeeded;
                }

                //new upgrade pop up + pause menu
                if (score.getScore() >= level2 && !boostUnlocked) //boost unlocked when score is 10
                {

                    //pop up
                    PopUp.popUpUpgradeActive = true; //pauses controls
                    rover.velocity = Vector2.zero; //freeze rover velocity
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("BoostInfo").gameObject.SetActive(true); //boost related info on pop up appears 
                    boostUnlocked = true;

                    //change progress bar goal for next upgrade
                    scoreNeeded = level3;
                    scoreNeededText.text = $"{scoreNeeded - score.getScore()} Points Needed to Upgrade!";
                    fill.fillAmount = (float)score.getScore() / scoreNeeded;

                    //update upgrades in pause menu
                    nextUpgradeText.text = "Transmitter"; //change to next upgrade
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("BoostInfo").gameObject.SetActive(true);

                }

                if (score.getScore() >= level3 && !transmitterUnlocked) //transmitter unlocked when score is 20***
                {
                    //pop up
                    PopUp.popUpUpgradeActive = true; //pauses controls
                    rover.velocity = Vector2.zero; //freeze rover velocity
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("TransmitterInfo").gameObject.SetActive(true); //transmitter related info on pop up appears 
                    transmitterUnlocked = true;

                    //change progress bar goal for next upgrade
                    scoreNeeded = level4;
                    scoreNeededText.text = $"{scoreNeeded - score.getScore()} Points Needed to Upgrade!";
                    fill.fillAmount = (float)score.getScore() / scoreNeeded;

                    //update upgrades in pause menu
                    nextUpgradeText.text = "Flashlight"; //change to whatever next upgrade is
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("TransmitterInfo").gameObject.SetActive(true);
                }
                if (score.getScore() >= level4 && !flashlightUnlocked) //flashlight unlocked when score is 30***
                {
                    //pop up
                    PopUp.popUpUpgradeActive = true; //pauses controls
                    rover.velocity = Vector2.zero; //freeze rover velocity
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("FlashlightInfo").gameObject.SetActive(true); //flashlight related info on pop up appears 
                    flashlightUnlocked = true;

                    //change progress bar goal for next upgrade
                    scoreNeeded = level5;
                    scoreNeededText.text = $"{scoreNeeded - score.getScore()} Points Needed to Upgrade!";
                    fill.fillAmount = (float)score.getScore() / scoreNeeded;

                    //update upgrades in pause menu
                    nextUpgradeText.text = "Grabber"; //change to whatever next upgrade is
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("FlashlightInfo").gameObject.SetActive(true);
                }
                if (score.getScore() >= level5 && !grabberUnlocked) //grabber claw unlocked when score is 40***
                {
                    //pop up
                    PopUp.popUpUpgradeActive = true; //pauses controls
                    rover.velocity = Vector2.zero; //freeze rover velocity
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("GrabberInfo").gameObject.SetActive(true); //grabber claw related info on pop up appears 
                    grabberUnlocked = true;

                    //change progress bar goal for next upgrade
                    scoreNeeded = 0;
                    scoreNeededText.text = $"All upgrades complete!";
                    scoreNeededText.rectTransform.position = scoreNeededText.rectTransform.position + new Vector3(0, -20, 0);
                    fill.fillAmount = 100;

                    //update upgrades in pause menu
                    nextUpgradeText.text = "All upgrades complete!"; //change to whatever next upgrade is
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("GrabberInfo").gameObject.SetActive(true);
                }

                //new element pop ups + pause menu
                switch (val) //switch determines which element was just picked up
                {
                    case 20: // rock was picked up
                        {
                            if (rockIsNew) //pop ups only appear the first time an element is picked up
                            {
                                newElementPopUp();  //setup new element pop up
                                popUpPanel.transform.Find("RockInfo").gameObject.SetActive(true); //Rock info on pop up appears 

                                //add rock info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_rock").Find("RockButton").gameObject.SetActive(true);

                                rockIsNew = false;
                            }
                            break;
                        }

                    case 375: // mica was picked up
                        {
                            if (micaIsNew) //pop ups only appear the first time an element is picked up
                            {
                                newElementPopUp();  //setup new element pop up
                                popUpPanel.transform.Find("MicaInfo").gameObject.SetActive(true); //mica info on pop up appears 

                                //add mica info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_mica").Find("MicaButton").gameObject.SetActive(true);

                                micaIsNew = false;
                            }
                            break;
                        }

                    case 75: // iron was picked up
                        {
                            if (ironIsNew) //pop ups only appear the first time an element is picked up
                            {
                                newElementPopUp();  //setup new element pop up
                                popUpPanel.transform.Find("IronInfo").gameObject.SetActive(true); //iron info on pop up appears 

                                //add iron info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_iron").Find("IronButton").gameObject.SetActive(true);

                                ironIsNew = false;
                            }
                            break;
                        }

                    case 100: // nickel was picked up
                        {
                            if (nickelIsNew)
                            {
                                newElementPopUp();  //setup new element pop up
                                popUpPanel.transform.Find("NickelInfo").gameObject.SetActive(true); //nickel info on pop up appears 

                                //add nickel info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_nickel").Find("NickelButton").gameObject.SetActive(true);

                                nickelIsNew = false;
                            }
                            break;
                        }

                    case 225: // olivine was picked up
                        {
                            if (olivineIsNew)
                            {
                                newElementPopUp();  //setup new element pop up
                                popUpPanel.transform.Find("OlivineInfo").gameObject.SetActive(true); //olivine info on pop up appears 

                                //add olivine info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_olivine").Find("OlivineButton").gameObject.SetActive(true);

                                olivineIsNew = false;
                            }
                            break;
                        }

                    case 150: // feldspar was picked up
                        {
                            if (feldsparIsNew) //pop ups only appear the first time an element is picked up
                            {
                                newElementPopUp();  //setup new element pop up
                                popUpPanel.transform.Find("FeldsparInfo").gameObject.SetActive(true); //feldspar info on pop up appears 

                                //add feldspar info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_feldspar").Find("FeldsparButton").gameObject.SetActive(true);

                                feldsparIsNew = false;
                            }
                            break;
                        }

                    case 750: // pyroxene was picked up
                        {
                            if (pyroxeneIsNew)
                            {
                                newElementPopUp();  //setup new element pop up
                                popUpPanel.transform.Find("PyroxeneInfo").gameObject.SetActive(true); //pyroxene info on pop up appears 

                                //add pyroxene info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_pyroxene").Find("PyroxeneButton").gameObject.SetActive(true);

                                pyroxeneIsNew = false;
                            }
                            break;
                        }

                    case 500: // quartz was picked up
                        {
                            if (quartzIsNew)
                            {
                                newElementPopUp();  //setup new element pop up
                                popUpPanel.transform.Find("QuartzInfo").gameObject.SetActive(true); //quartz info on pop up appears 

                                //add quartz info button to pause menu
                                popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_quartz").Find("QuartzButton").gameObject.SetActive(true);

                                quartzIsNew = false;
                            }
                            break;
                        }
                }

            }
        }
        if (collision.gameObject.tag == "Journal")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (collision.gameObject.name == "NASAFactBook") //if reading nasa journal
                {
                    newJournalPopUp(); //pause controls and activate popup

                    if (numberOfPages > 1) // if user has found any pages, open journal to fact 1. otherwise, initial text is shown
                    {
                        journal.transform.Find("Fact1").gameObject.SetActive(true);
                    }

                    //set text objects for nasa journal
                    journal.transform.Find("NewJournalTipText").gameObject.SetActive(false);
                    journal.transform.Find("NewFactText").gameObject.SetActive(false);
                    journal.transform.Find("RereadFactText").gameObject.SetActive(true);

                    //switch buttons
                    journal.transform.Find("OKButton").gameObject.SetActive(false);
                    journal.transform.Find("CloseJournalButton").gameObject.SetActive(true);

                    if (numberOfPages > 2) //if user has collected more than one page, show next button
                    {
                        journal.transform.Find("NextButton").gameObject.SetActive(true);
                    }

                }
                else //if picking up new journal
                { 
                    //store coordinates of the collected element to list
                    Vector3 coordinates = collision.gameObject.GetComponent<FactBookScript>().getCoordinates();
                    journalCoordinates.Add(coordinates);

                    //collect journal
                    collision.gameObject.GetComponent<FactBookScript>().getFact();

                    switch (numberOfPages)
                    {
                        case 1:
                            {
                                newJournalPopUp();
                                journal.transform.Find("InitialText").gameObject.SetActive(false);
                                journal.transform.Find("Fact1").gameObject.SetActive(true);
                                numberOfPages++;
                                break;

                            }
                        case 2:

                            {

                                newJournalPopUp();
                                journal.transform.Find("Fact2").gameObject.SetActive(true);
                                numberOfPages++;
                                break;
                            }
                        case 3:

                            {

                                newJournalPopUp();
                                journal.transform.Find("Fact3").gameObject.SetActive(true);
                                numberOfPages++;
                                break;
                            }
                        case 4:

                            {

                                newJournalPopUp();
                                journal.transform.Find("Fact4").gameObject.SetActive(true);
                                numberOfPages++;
                                break;
                            }
                        case 5:

                            {

                                newJournalPopUp();
                                journal.transform.Find("Fact5").gameObject.SetActive(true);
                                numberOfPages++;
                                break;
                            }
                    }
                }
           
            }
        }
    }
    //prepares new element popup before specific element info appears
    private void newElementPopUp()
    {
        PopUp.popUpElementActive = true; //pauses controls
        rover.velocity = Vector2.zero; //freeze rover velocity
        popUpPanel.gameObject.SetActive(true); //pop up appears
        popUpPanel.transform.Find("NewElementText").gameObject.SetActive(true); //new element text appears
        return;
    }

    private void newJournalPopUp()
    {
        PopUp.popUpFactsActive = true; //pauses controls
        rover.velocity = Vector2.zero; //freeze rover velocity
        journal.gameObject.SetActive(true); //pop up appears
        
        return;
    }

    public void setSpawn(Vector3 coordinates) // sets coordinates for rover returning to hubworld
    {
        coordinates.y -= 3; // move slightly down from teleporter

        if (SceneManager.GetActiveScene().name == "HubWorld")
        {
            caveCoordinates = coordinates;  //set hub world coordinates for moveSpawn
        }
        else if (SceneManager.GetActiveScene().name == "Cave4")
        {
            cave4Coordinates = coordinates; //set cave 4 coordinates for moveSPawn
        }
    }

    public void moveSpawn() //moves the rover position to cavecoordinates
    {
        if (SceneManager.GetActiveScene().name == "HubWorld")
        {
            gameObject.transform.position = caveCoordinates;    // move rover to caveCooridnates

            //rover should face up on first load of game, but down after exiting any other cave into hub world
            if (!rockIsNew) //if this is not the first load (since user has to pick up rock to exit hubworld)
            {
                setSpawnDirection(); //face down
            }
        }
        else if (SceneManager.GetActiveScene().name == "Cave4")
        {
            gameObject.transform.position = cave4Coordinates;    // move rover to cave4Cooridnates
            setSpawnDirection(); // face down
        }
    }

    public void setSpawnDirection() //sets rover to face down on load, or up if in win cave
    {
        // set floats to "down" direction
        animator.SetFloat("MovementX", 0);
        animator.SetFloat("MovementY", -1);

        //if in win cave, set floats to "up" direction
        if (SceneManager.GetActiveScene().name == "CaveWin")
        {
            animator.SetFloat("MovementY", 1);
        }

        animator.Play(animationLevel + "Idle");
    }
}

