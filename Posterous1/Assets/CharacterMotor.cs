using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    //player's Animations
    private Animator animations;

    //Speed movement
    public float walkspeed;
    public float runspeed;
    public float turnSpeed;

    //Inputs
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;
    public Vector3 jumpSpeed;
  
    CapsuleCollider playerCollider;
    
    bool IsGrounded()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        return Physics.Raycast(transform.position, dwn, 0.4f);
    }

    // Start is called before the first frame update
    void Start()
    {
        //animations = gameObject.GetComponent<Animation>();
        playerCollider = GetComponent<CapsuleCollider>();
        animations = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // gravity
        if (!IsGrounded())
        { 
            transform.Translate(0, -0.2f*Time.deltaTime, 0);
        }
        // move forward
        if (Input.GetKey(inputFront))
        {
            transform.Translate(0, 0, walkspeed * Time.deltaTime);
            //animations.Play("Walking");
        }

        // move backward
        if (Input.GetKey(inputBack))
        {
            transform.Translate(0, 0, -(walkspeed/2) * Time.deltaTime);

            //animations.Play("Walking");
        }

        // left rotate
        if (Input.GetKey(inputLeft))
        {
            transform.Rotate(0, -turnSpeed*Time.deltaTime, 0);
        }

        // right rotate
        if (Input.GetKey(inputRight))
        {
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            
        }

        // sprint
        if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(0, 0, runspeed * Time.deltaTime);
            //animations.Play("Running");
        }

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
            v.y = jumpSpeed.y;

            gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
            animations.SetTrigger("Jump");
        }
        // Animations
        if (Input.GetKey(inputFront) & !Input.GetKey(KeyCode.LeftShift))
        {
            animations.SetBool("Walk", true);
            animations.SetBool("Run", false);

        }
        if (Input.GetKey(inputBack) & !Input.GetKey(KeyCode.LeftShift))
        {
            animations.SetBool("Walk", true);
            animations.SetBool("Run", false);

        }
        if (Input.GetKey(inputFront) & Input.GetKey(KeyCode.LeftShift))
        {
            animations.SetBool("Walk", false);
            animations.SetBool("Run", true);
        }
        if (!Input.GetKey(inputFront) & !Input.GetKey(inputBack))
        {
            animations.SetBool("Walk", false);
            animations.SetBool("Run", false);
        }
    }
}
