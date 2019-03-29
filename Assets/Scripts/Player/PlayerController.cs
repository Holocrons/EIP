using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int x = 0;
    public int speed = 5;
    public Transform groundChecker;
    private bool isGrounded = true;
    private Rigidbody2D rb;
    private int layerMask;
    public float jumpVelocity = 10;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        layerMask = 1 << 10;
        layerMask = ~layerMask;
    }

    private void Update()
    {
        Move();
        GroundRaycast();
        Jump();   
        anim.SetBool("jumping", !isGrounded);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Debug.Log("I m in");
            rb.velocity = Vector2.up * jumpVelocity * Time.deltaTime;
        }
        if (rb.velocity.y < 0 && isGrounded == false)
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.deltaTime;
        else if (rb.velocity.y > 0 && isGrounded == false)
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1f * Time.deltaTime;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
            anim.SetBool("running", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1;
            anim.SetBool("running", true);
        }
        else
        {
            x = 0;
            anim.SetBool("running", false);
        }
        transform.Translate(new Vector2(x, 0) * Time.deltaTime * speed);
    }

    private void GroundRaycast()
    {
        RaycastHit2D hit;


        hit = Physics2D.Raycast(groundChecker.position, -Vector2.up, 0.5f, layerMask);
        Debug.DrawRay(groundChecker.position, new Vector2(0, -0.5f));

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }














    /*// Movement is handled by a separate script
    public PlayerMovement pm;

    public float movementSpeed = 100;
    public float jumpHeight = 35;
    public int maxJumps = 1;
    public int resetOnWallLatch = 1;


    private Rigidbody2D rb;
    private bool airborne = false;

    private bool facingLeft = false;
    private float horizontalMovement;

    private bool requestJump = false;
    private int jumps = 0;
    private Animator anim;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Terrain-Ground"))
        {
            jumps = 0;
            airborne = false;
            anim.SetBool("jumping", airborne);
        }
        if (collision.gameObject.tag.Contains("Terrain-Wall"))
        {             
            if (jumps > 0)
                jumps -= resetOnWallLatch;
        }
    }

    // Update is called once per frame
    void Update () {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;

        if (jumps < maxJumps && Input.GetButtonDown("Jump"))
        {
            requestJump = true;
        }

        if (horizontalMovement > 0 && facingLeft)
        {
            DoAFlip();
        } else if (horizontalMovement < 0 && !facingLeft)
        {
            DoAFlip();
        }
    }

    void FixedUpdate()
    {
        pm.Move(horizontalMovement, requestJump, airborne);
        if (requestJump)
        {
            jumps++;
            requestJump = false;
        }
    }

    void DoAFlip()
    {
        facingLeft = !facingLeft;

        Vector3 spriteScale = transform.localScale;
        spriteScale.x *= -1;
        transform.localScale = spriteScale;
    }*/
}
