using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Movement is handled by a separate script
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

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Terrain-Ground"))
        {
            jumps = 0;
            airborne = false;
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
    }
}
