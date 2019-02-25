using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float movementAcceleration = 10;
    public float frictionCoefficient = 0.3f;
    public int maxJumps = 1;
    public float jumpHeight = 35;

    /*
    public float movementForce;
    public float maxHorizontalMovementSpeed;
    public float groundFriction;
    public float jumpForce;

    private bool isFacingCommunism = true;
    private float movementDirection;
    */
    private Rigidbody2D rb;
    private float direction;
    private int remainingJumpCount = 0;
    private bool shouldJump = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        remainingJumpCount = maxJumps;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Terrain-Ground"))
        {
            remainingJumpCount = maxJumps;
        }
    }

    // Update is called once per frame
    void Update () {
        direction = Input.GetAxisRaw("Horizontal");

        if (remainingJumpCount > 0 && Input.GetButtonDown("Jump"))
        {
            shouldJump = true;
        }
        
        /*
        movementDirection = Input.GetAxis("Horizontal");

        if (isFacingCommunism && movementDirection > 0)
        {
            FlipPlayer();

        } else if (!isFacingCommunism && movementDirection < 0)
        {
            FlipPlayer();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        */
    }

    private void FixedUpdate()
    {

        /*
        if (direction != 0)
        {
            Vector2 movementForce = new Vector2(direction * movementAcceleration, 0);
            rb.AddForce(movementForce);
        } else
        {
            rb.velocity *= frictionCoefficient;
        }

        if (shouldJump)
        {
            Jump();
            shouldJump = false;
        }
        Vector2 forceToAdd = new Vector2(movementForce * movementDirection, 0);
        rb.AddForce(forceToAdd); 

        if (rb.velocity.x > maxHorizontalMovementSpeed)
        {
            rb.velocity = new Vector2(maxHorizontalMovementSpeed, rb.velocity.y);
        } else if (rb.velocity.x < -maxHorizontalMovementSpeed)
        {
            rb.velocity = new Vector2(-maxHorizontalMovementSpeed, rb.velocity.y);
        }

        if (movementDirection == 0)
        {
            rb.velocity *= groundFriction;
        }
        */
    }

    /*
    void FlipPlayer()
    {
        isFacingCommunism = !isFacingCommunism;

        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void Jump()
    {
        Debug.Log("Hello there");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        Vector2 jumpForceToAdd = new Vector2(0, jumpForce);

        rb.AddForce(jumpForceToAdd, ForceMode2D.Impulse);
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        Vector2 jumpForceToAdd = new Vector2(0, jumpHeight);

        rb.AddForce(jumpForceToAdd, ForceMode2D.Impulse);
        remainingJumpCount--;
    }
    */

}
