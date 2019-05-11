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
    private float damageTimer = 0;
    private bool isHit = false;

    public int hp = 10;
    public float attackCd = 0.5f;
    public float attackCd2 = 0.5f;
    public GameObject attackBox;
    public GameObject dmgImage;
    public GameObject healthBar;

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
            
            attackBox.SetActive(attacking);
        }
        if (attacking == true && attackTimer <= Time.time)
        {
            anim.SetBool("attacking", false);
            attacking = false;
            attackBox.SetActive(attacking);
            attackTimer2 = Time.time + attackCd2;
        }
        if (attacking == true)
        {
            attackBox.transform.localPosition = new Vector2(0, 0);
            if (attackBox.transform.parent.transform.GetComponent<SpriteRenderer>().flipX)
                attackBox.transform.localScale = new Vector2(-1 , 1);
            else
                attackBox.transform.localScale = new Vector2(1, 1);
        }
        if (hp <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            this.enabled = false;
        }
        if (isHit == true && damageTimer <= Time.time)
        {
            GetComponent<PlayerController>().enabled = true;
            isHit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "attackBox" && isHit == false)
        {
            hp -= 1;
            GameObject tmp = Instantiate(dmgImage);
            tmp.transform.parent = healthBar.transform;
            tmp.GetComponent<RectTransform>().localPosition = new Vector3(30 + hp * 50, -20, 0);          
            float bump = 7f;
            if (transform.position.x < collision.transform.position.x)
                bump = -7f;
            rb.AddForce(new Vector2(bump ,5) * Time.deltaTime * 250, ForceMode2D.Impulse);
            GetComponent<PlayerController>().enabled = false;
            damageTimer = Time.time + 0.75f;
            isHit = true;
           
        }
    }

}
