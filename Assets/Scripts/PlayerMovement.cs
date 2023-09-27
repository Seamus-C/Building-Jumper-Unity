using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**************************************************
* attached to player: moves player in direction of player input W/A/S/D 
* allows player to jump on jump input (spacebar)
* launches player upon collision with launchpad tagged game object
*
*   Bryce Haddock 1.0 September 25, 2023
**************************************************/
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float launchForce;
    public float boostForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public Scorekeeper scorekeeper;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    public Transform startPosition;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Initializes ready to jump and player rigidbody on start
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    //Updates method every frame 
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround); //Searches for ground on frame update if ground is detected player = grounded

        MyInput();
        SpeedControl();

        if (grounded)  //eliminates drag when not touching ground
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }

    //countinuesly checks the move player field 
    private void FixedUpdate()
    {
        MovePlayer();
    }

    // executes method on specified player input
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // moves player horizontal on horizantal input A/D
        verticalInput = Input.GetAxisRaw("Vertical");   // moves player vertical on vertical input W/S

        if (Input.GetKey(jumpKey) && readyToJump && grounded) //if the player is ready to jump and grounded while jumpkey(spacebar) is pressed the following is executed
        {
            readyToJump = false;    //player is no longer ready to jump

            Jump(); // jump action is performed

            Invoke(nameof(ResetJump), jumpCooldown);    //jump cooldown begins 
        }
    }

    // when the player collides with the launch pad tagged object they will perform the launch action
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LaunchPad"))
        {
            LaunchPadController launchPad = collision.gameObject.GetComponent<LaunchPadController>();

            if (launchPad != null)
            {
                launchForce = launchPad.launchMultiplier;
            }


            Launch();
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
        /*if (collision.gameObject.CompareTag("Booster"))
        {
            Boost();
        }*/
    }
    // moves the player in the relative direction of player input
    // if the player is grounded they move at normal move speed
    //if the player is not grounded they move at normal speed times air multiplier
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    //limits player movement speed to player movement speed 
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    // when the jump action is performed the player will launch into the air at jump speed
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // when the launch action is invoked the player will launch into the air at launch speed
    private void Launch()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * launchForce, ForceMode.Impulse);
    }

    /*private void Boost()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
    }*/
    // resets ready to jump 
    private void ResetJump()
    {
        readyToJump = true;
    }
}
    
