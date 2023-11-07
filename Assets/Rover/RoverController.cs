using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

public class RoverController : MonoBehaviour
{
    // external objects assigned from inspector
    public GameObject scoreboard;

    // external objects assigned from script 
    //private GameObject element;

    // rover related objects and settings
    private Rigidbody2D rover;
    private float speed_init;
    private float speed;
    private int boost;
    private Animator animator;
    private Vector2 input;


    // Start is called before the first frame update
    void Start()
    {
        // init objects
        rover = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        // init settings
        speed_init = 150.0f;
        boost = 5;
        speed = speed_init;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            Move();
            Animate();
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

        // this helps ensure no animation change in rover for no input
        if (horizontal == 0 && vertical == 0) 
        {
            rover.velocity = Vector2.zero;
            return;
        }

        // speed boost while pressing space
        // BUG - able to lock into boost when input has 2 directions at once
        // QUICKFIX - added "spped_init" so rover doesn't keep getting faster
        if (Input.GetKeyDown(KeyCode.Space) && speed == speed_init) 
        {
            speed *= boost;
        }
        if (Input.GetKeyUp(KeyCode.Space) && speed > speed_init)
        {
            speed /= boost;
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
                scoreboard.GetComponent<ScoreBoard>().adjustScore(val);   
            }
        }
    }


    /*
     * Probablly don't need.
     *
     

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Element")
        {
            element = collision.gameObject;
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.Equals(element))
        {
            element = null;
        }
    }


    */

}
