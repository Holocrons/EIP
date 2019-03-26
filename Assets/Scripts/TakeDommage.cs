using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDommage : MonoBehaviour
{
    private Animator anim;
    private float attackTimer = 0;
    private bool attacking = false;
    private float attackTimer2 = 0;
    private Rigidbody2D rb;


    public int hp = 10;
    public float attackCd = 0.5f;
    public float attackCd2 = 0.5f;
    public GameObject attackBox;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer2 <= Time.time && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("attacking", true);
            attackTimer = Time.time + attackCd;
            attacking = true;
        }
        if (attacking == true && attackTimer <= Time.time)
        {
            anim.SetBool("attacking", false);
            attacking = false;
            attackTimer2 = Time.time + attackCd2;
        }
        attackBox.SetActive(attacking);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "attackBox")
        {
            hp -= 1;
            int bump = -3;
            if (transform.position.x < collision.transform.position.x)
                bump = 3;
            //rb.AddForce(new Vector2(bump ,3) * Time.deltaTime * 500, ForceMode2D.Impulse);
        }
    }

}
