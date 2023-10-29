using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

public class RoverController : MonoBehaviour
{
    public float speed = 5.0f;

    private Rigidbody2D rover;
    private Animator animator;
    private Vector2 input;


    // Start is called before the first frame update
    void Start()
    {
        rover = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Animate();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 0 && vertical == 0) 
        {
            rover.velocity = Vector2.zero;
            return;
        }

        input = new Vector2(horizontal, vertical);
        rover.velocity = input * speed * Time.fixedDeltaTime;
    }

    private void Animate()
    {
        animator.SetFloat("MovementX", input.x);
        animator.SetFloat("MovementY", input.y);
    }
}
