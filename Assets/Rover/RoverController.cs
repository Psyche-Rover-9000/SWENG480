using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

public class RoverController : MonoBehaviour
{
    // external objects assigned from inspector
    public GameObject scoreboard;
    public AudioSource collectSFX;                 

    // rover related objects and settings
    private Rigidbody2D rover;
    private float speed_init;
    private float speed;
    private float boost;
    private Animator animator;
    private Vector2 input;
    private int rover_level;
    private ScoreBoard score;
    private string anim_lvl;

    //pop up related objects and variables
    public GameObject popUpPanel;
    bool ironIsNew = true;
    public GameObject upgradePopUp;
    bool boostUnlocked = false;

    //score progress objects and variables
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI scoreNeededText;
    public Image fill;
    public int scoreNeeded = 10; //initialized for first upgrade (boost)

    //upgrades in pause menu objects and variables
    public TextMeshProUGUI nextUpgradeText;


	//creates an instance of the player rover
    private static RoverController roverInstance = null;
    private void Awake()
    {
        if (roverInstance == null)
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
        rover_level = 1;
        anim_lvl = "L1_";
        
    }

    // Update is called once per frame
    void Update()
    {

        if (rover_level == 1 && score.getScore() >= 10)
        {
            rover_level = 2;
            anim_lvl = "L2_";
        }

        if (rover_level == 2 && score.getScore() >= 20)
        {
            rover_level = 3;
            anim_lvl = "L3_";

        }

        

        if (!PauseMenu.isPaused)
        {
            if (!PopUp.popUpActive)
            {
                Move();
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        // without motion input, idle without changing direction
        if (horizontal == 0 && vertical == 0)
        {
            animator.Play(anim_lvl + "idle");
            rover.velocity = Vector2.zero;
            return;
        }

        // move and animate rover to match input
        input = new Vector2(horizontal, vertical);
        animator.SetFloat("MovementX", input.x);
        animator.SetFloat("MovementY", input.y);
        if(rover_level > 1 && Input.GetKey(KeyCode.Space))
        {
            speed = boost;
            animator.Play(anim_lvl + "boost");
        }
        else
        {
            speed = speed_init;
            animator.Play(anim_lvl + "move");
        }
        rover.velocity = input * speed * Time.fixedDeltaTime;
        


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
                scoreNeededText.text = $"{scoreNeeded - score.getScore()} Point(s) Needed to Upgrade!";
                fill.fillAmount = (float)score.getScore() / scoreNeeded;

                //new upgrade pop up
                if (score.getScore() > 10 && !boostUnlocked) //boost unlocked when score is 10
                {

                    //pop up
                    PopUp.popUpActive = true; //pauses controls
                    upgradePopUp.gameObject.SetActive(true); //pop up appears
                    upgradePopUp.transform.Find("BoostInfo").gameObject.SetActive(true); //boost related info on pop up appears 
                    boostUnlocked = true;

                    //change progress bar goal for next upgrade ********

                    //update upgrades in pause menu
                    nextUpgradeText.text = "something else"; //change to whatever next upgrade is
                    popUpPanel.transform.parent.Find("PausePanel").Find("UpgradeInfo").Find("UnlockedUpgradesText").Find("BoostInfo").gameObject.SetActive(true);

                }

                //pop up menu:
                //once there are more elements, add switch here for which element is being collected.
                //within iron block of switch:
                if (ironIsNew)
                {
                    PopUp.popUpActive = true; //pauses controls
                    popUpPanel.gameObject.SetActive(true); //pop up appears
                    popUpPanel.transform.Find("IronInfo").gameObject.SetActive(true); //iron info on pop up appears 

                    //add iron info button to pause menu
                    popUpPanel.transform.parent.Find("PausePanel").Find("ElementBlocks").Find("ElementBlock_iron").Find("IronButton").gameObject.SetActive(true);

                    ironIsNew = false;
                }

            }
        }
    }
    /*
     * Checks if objects in game have tag "Breakable"
     * If so the Rover can Destroy these objects and move on
     * 
     */
    private void OnTriggerStay2D(Collider2D other)
    {
        if (score.getScore() > 15)
        {
            if (other.gameObject.tag == "Breakable")

            {
                Destroy(other.gameObject);
            }

        }
    }

}
