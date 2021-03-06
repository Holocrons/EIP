﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Need for ground collision check
    public Transform groundChecker;
    private bool isGrounded = true;

    // Jump configuration and tracking
    public float jumpVelocity = 2000;
    public int maxJumps = 2;
    private int currentJumps = 0;

    // Movement speed
    public int speed = 15;
    // Default direction the character sprite is looking at
    public bool lookingRight = true;
    private int movementDirection = 0;

    // Wall collision and attachment
    private bool wallLatch = false;
    private int latchDirection = 0;

    // Player dash
    public float dashDistance = 350;
    public int dashCooldown = 3000;
    private float dashRemainingCooldown = 0;

    private int layerMask;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        layerMask = 1 << 10;
        layerMask = ~layerMask;
    }

    private void Update()
    {
        if (dashRemainingCooldown > 0)
            dashRemainingCooldown -= Time.deltaTime * 1000;
    }

    private void FixedUpdate()
    {
        GroundRaycast();
        Jump();
        Move();
        anim.SetBool("jumping", !isGrounded);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && maxJumps > currentJumps)
        {
            rb.velocity = Vector2.up * jumpVelocity * Time.fixedDeltaTime;
            currentJumps++;
            //wallLatch = false;
            /*
            if (wallLatch)
                rb.gravityScale = defaultGravity;
            */
            // Debug.Log("Jumps : " + currentJumps);
        }

        // If the player is falling, increase the gravity for a smoother jump
        if (rb.velocity.y < 0 && isGrounded == false)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1.5f * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && isGrounded == false)
            rb.velocity += Vector2.up * Physics2D.gravity.y * 1f * Time.fixedDeltaTime;
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection = 1;
            anim.SetBool("running", true);
            lookingRight = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movementDirection = -1;
            anim.SetBool("running", true);
            lookingRight = false;
        }
        else
        {
            movementDirection = 0;
            anim.SetBool("running", false);
        }

        sp.flipX = !lookingRight;

        if (dashRemainingCooldown <= 0 && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(new Vector2(lookingRight ? 1 : -1, 0) * Time.fixedDeltaTime * dashDistance);
            dashRemainingCooldown = dashCooldown;
        }


        if (!wallLatch || movementDirection == latchDirection && wallLatch)
            transform.Translate(new Vector2(movementDirection, 0) * Time.fixedDeltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // https://answers.unity.com/questions/783377/detect-side-of-collision-in-box-collider-2d.html

        /*
         * Most of this function is currently useless or rather unused.
         * The main goal of this segment is to detect on which side the character touches a walls
         * This is needed for the "wall-latch" mechanic and not runnig into walls
         */

        Collider2D collider = collision.collider;
        bool collideFromLeft = false;
        bool collideFromTop = false;
        bool collideFromRight = false;
        bool collideFromBottom = false;
        float RectWidth = this.GetComponent<Collider2D>().bounds.size.x;
        float RectHeight = this.GetComponent<Collider2D>().bounds.size.y;
        float circleRad = collider.bounds.size.x;

        if (collision.gameObject.tag.Contains("Terrain-Wall"))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collider.bounds.center;

            if (contactPoint.y > center.y && //checks that circle is on top of rectangle
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2))
            {
                collideFromTop = true;
            }
            else if (contactPoint.y < center.y &&
                (contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2))
            {
                collideFromBottom = true;
            }
            else if (contactPoint.x > center.x &&
                (contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2))
            {
                collideFromRight = true;
            }
            else if (contactPoint.x < center.x &&
                (contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2))
            {
                collideFromLeft = true;
            }

            if (currentJumps > 0)
                currentJumps -= 1;
            wallLatch = true;
            if (collideFromLeft)
                latchDirection = -1;
            if (collideFromRight)
                latchDirection = 1;
        }

    }

    private void GroundRaycast()
    {
        RaycastHit2D hit;


        hit = Physics2D.Raycast(groundChecker.position, -Vector2.up, 0.5f, layerMask);
        // Debug.DrawRay(groundChecker.position, new Vector2(0, -0.5f));

        if (hit.collider != null)
        {
            isGrounded = true;
            /*
            if (wallLatch)
                rb.gravityScale = defaultGravity;
            */
            wallLatch = false;
            if (currentJumps != 0)
            {
                currentJumps = 0;
                // Debug.Log("Jumps reset !");
            }
        }
        else
        {
            isGrounded = false;
        }
    }














    /*// Movement is handled by a separate script
    public PlayerMovement pm;
    public Sprite standing;
    public Sprite crouched;

    public float movementSpeed = 100;
    public float jumpHeight = 35;
    public int maxJumps = 1;
    public int resetOnWallLatch = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool airborne = false;
    private bool isCrouched = false;

    private bool facingLeft = true;
    private float horizontalMovement;

    private bool requestJump = false;
    private int jumps = 0;
    private Animator anim;

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        //Component.FindChi
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            isCrouched = true;
            sr.sprite = crouched;
        }
        if (isCrouched && Input.GetKeyUp(KeyCode.S))
        {
            isCrouched = false;
            sr.sprite = standing;
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
