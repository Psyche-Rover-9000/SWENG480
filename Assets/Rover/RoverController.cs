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
    private bool boost_enabled;
    private ScoreBoard score;

    //pop up related objects and variables
    public GameObject popUpPanel;
    bool ironIsNew = true;

    //score prgress objects
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI scoreNeededText;
    public Image fill;
    public int scoreNeeded = 10; //initialized for first upgrade

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
        boost_enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!boost_enabled && score.getScore() >= 10)
        {
            boost_enabled = true;
        }


        if (!PauseMenu.isPaused)
        {
            if (!PopUp.popUpActive)
            {
                Move();
                Animate();
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

        // speed boost while pressing space
        if (boost_enabled && Input.GetKeyDown(KeyCode.Space)) 
        {
            speed = boost;
        }
        if (boost_enabled && Input.GetKeyUp(KeyCode.Space))
        {
            speed = speed_init;
        }


        // this helps ensure no animation change in rover for no input
        if (horizontal == 0 && vertical == 0)
        {
            rover.velocity = Vector2.zero;
            return;
        }

        // move the rover
        input = new Vector2(horizontal, vertical);
        rover.velocity = input * speed * Time.fixedDeltaTime;


    }

    /*
     * Animate
     *  Changes the rover sprite for directional movement using the animator component.
     */
    private void Animate()
    {
        animator.SetFloat("MovementX", input.x);
        animator.SetFloat("MovementY", input.y);
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
                scoreNeededText.text = $"{scoreNeeded-score.getScore()} Point(s) Needed to Upgrade!";
                fill.fillAmount = (float) score.getScore() / scoreNeeded;

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


}
