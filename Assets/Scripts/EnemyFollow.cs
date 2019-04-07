using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    private int x = 0;
    public float range = 5f;
    private bool isFacingRight = false;
    public float attackRange = 1f;
    private bool attacking = false;
    public float attackCd = 0.5f;
    private float attackTimer = 0;
    public GameObject attackBox;
    private Animator anim;
    private Rigidbody2D rb;
    private bool bump = false;
    private float bumpTimer = 0;

    public Transform target;
    public int hp = 3;
    public float bumpDuration = 0.5f;


    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (attackTimer < Time.time)
        {
            attacking = false;
            attackBox.SetActive(false);
        }
        if (target.position.x > transform.position.x && attacking == false)
        {
            x = 1;
        }
        else if (attacking == false)
        {
            x = -1;
        }
        flipSprite(x);
        enemyAggro(x);   
    }

    void flipSprite(int x)
    {
        if (((x > 0 && !isFacingRight) || (x < 0 && isFacingRight)) && Vector2.Distance(transform.position, target.position) >= 3)
        {
            isFacingRight = !isFacingRight;
            Vector2 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    void enemyAggro(int x)
    {
        float dist = Vector2.Distance(transform.position, target.position);

        if (dist <= range && dist > attackRange && attacking == false && bump == false)
        {
            transform.Translate(new Vector2(x, 0) * speed * Time.deltaTime);
            anim.SetBool("running", true);
        }
        else if (dist <= attackRange && attacking == false)
        {
            attacking = true;
            attackTimer = Time.time + attackCd;
            attackBox.SetActive(true);
            anim.SetBool("running", false);
        }
        else
        {
            anim.SetBool("running", false);
        }
        anim.SetBool("attacking", attacking);
        if (bump == true && bumpTimer <= Time.time)
            bump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "dommage")
        {
            hp -= 1;
            bump = true;
            if (hp <= 0)
                Destroy(this.gameObject);
            bumpTimer = bumpDuration + Time.time;
            rb.AddForce(new Vector2(transform.localScale.x, 3) * Time.deltaTime * 50, ForceMode2D.Impulse);
        }
    }
}    




