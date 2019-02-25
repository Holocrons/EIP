using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PlayerController pc;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	void Update () {
	}

    void FixedUpdate()
    {
    }

    public void Move(float move, bool jump, bool airborne)
    {
        if (move != 0)
        {
            if (airborne)
            {
                rb.AddForce(new Vector2(move, 0));
            } else
            {
                Vector2 pos = transform.position;
                pos.x += move;
                transform.position = pos;
            }
        }
        else
        {
            // rb.velocity = new Vector2(0.6f, rb.velocity.y);
        }

        if (jump)
        {
            rb.AddForce(new Vector2(0f, pc.jumpHeight), ForceMode2D.Impulse);
            airborne = true;
        }
    }
}
