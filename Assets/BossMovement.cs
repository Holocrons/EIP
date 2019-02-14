using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float x = 0;
    private float y = 0;
    public float maxDist;
    bool isMoving = false;
    private int nb = 0;
    public float speed = 5;
    private float timer = 0;
    public GameObject playerBis;
    private Vector3 hitpos;
    public List<GameObject> follow;
    public bool debug = false;
    public List<GameObject> Arms;
    private float maxHight;
    private float minHight;
    private int nbLimbs = 4;
    private ArmManager firstLimb;
    private ArmManager secondLimb;
    public GameObject attackFolow;
    private bool going = false;
    public Transform rotationCenter;
    private float angularSpeed = 5;
    private float attackRadus = 10;
    private float angle = 0;
    private bool smashing = false;
    public GameObject projectiles;
    public GameObject player;
    private bool shooting = false;
    private float timerShoot = 0;
    private int shootCount = 0;


    // Use this for initialization
    void Start()
    {

        firstLimb = Arms[0].GetComponent<ArmManager>();
        secondLimb = Arms[1].GetComponent<ArmManager>();
        maxHight = 8.1f;
        minHight = 7.9f;
        hitpos = new Vector3(-1000, -1000);
        playerBis.transform.position = transform.position;
        nbLimbs = firstLimb.armPrefabs.Count + secondLimb.armPrefabs.Count;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;

        Debug.Log(rotationCenter.transform.position.x);
    
        nbLimbs = firstLimb.armPrefabs.Count + secondLimb.armPrefabs.Count;
        if (nbLimbs <= 3 && nbLimbs > 1)
        {
            maxHight = 5.6f;
            minHight = 5.4f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        else
        {
            x = 0;
        }
        hit = Physics2D.Raycast(transform.position, -Vector2.up, maxDist);
        if (hit.collider != null && Vector2.Distance(transform.position, hit.transform.position) < minHight)
        {
            y = 1;
        }
        else if (hit.collider != null && Vector2.Distance(transform.position, hit.transform.position) > maxHight)
        {
            y = -1;
        }
        else
        {
            y = 0;
        }
        if (x == 0)
            isMoving = false;
        else
            isMoving = true;
        if (smashing == false)
            transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E) || going == true || smashing == true || shooting == true)
        {
            //CircularAttack();
            //SmashAttack(hit);
            ThrowThings();
        }
        Raycast();
        if (hitpos != new Vector3(-1000, -1000))
            follow[nb].transform.position = Vector2.MoveTowards(follow[nb].transform.position, hitpos, speed * Time.deltaTime * 8f);
        x = 0;
        y = 0;
    }

    void ThrowThings()
    {
        GameObject tmp;
        if (timerShoot < Time.time)
        {
            tmp = Instantiate(projectiles, transform.position, transform.rotation);
            tmp.GetComponent<Shoot>().parent = this.gameObject;
            tmp.GetComponent<Shoot>().player = player;
            timerShoot = Time.time + 0.2f;
            shooting = true;
            shootCount++;
        }
        if (shootCount >= 3)
        {
            shootCount = 0;
            shooting = false;
        }
    }

    void SmashAttack(RaycastHit2D hit)
    {
        smashing = true;
        transform.Translate(new Vector2(0, -1) * speed * 6 * Time.deltaTime);
        if (hit.collider != null && Vector2.Distance(transform.position, hit.transform.position) < 2)
        {
            smashing = false;
        }
    }

    void CircularAttack()
    {
        if (going == false)
        {
            attackFolow.transform.position = new Vector2(transform.position.x, transform.position.y + 15);
            going = true;
        }
        float posx = rotationCenter.position.x + Mathf.Cos(angle) * attackRadus;
        float posy = rotationCenter.position.y + Mathf.Sin(angle) * attackRadus;
        attackFolow.transform.position = new Vector2(posx, posy);
        angle = angle + Time.deltaTime * angularSpeed;
        if (angle >= 8)
        {
            attackFolow.transform.position = transform.position;
            going = false;
            angle = 0;
        }
        /*timerAttack += Time.deltaTime;
        float x = (attackFolow.transform.position.x + Mathf.Cos(timerAttack)) / 1.1f;
        float y = (attackFolow.transform.position.y + Mathf.Sin(timerAttack)) / 1.1f;
        attackFolow.transform.position = new Vector2(x, y);*/
    }

    Vector2 getClosestPos()
    {
        Vector2 tmp = new Vector3(-1000, -1000);
        RaycastHit2D hit;
    
        if (nb == 1)
        {
            if (x > 0)
            {
                hit = Physics2D.Raycast(transform.position, -Vector2.up, maxDist);
                if (hit.collider != null && hit.collider.tag != "bone")
                    tmp = hit.point;
            }
            else
            {
                hit = Physics2D.Raycast(new Vector2(transform.position.x - 6, transform.position.y), -Vector2.up, maxDist);
                if (hit.collider != null && hit.collider.tag != "bone")
                    tmp = hit.point;
            }
        }
        else if (nb == 0)
        {
            if (x > 0)
            {
                hit = Physics2D.Raycast(new Vector2(transform.position.x + 6, transform.position.y), -Vector2.up, maxDist);
                if (hit.collider != null && hit.collider.tag != "bone")
                    tmp = hit.point;
            }
            else
            {
                hit = Physics2D.Raycast(transform.position, -Vector2.up, maxDist);
                if (hit.collider != null && hit.collider.tag != "bone")
                    tmp = hit.point;
            }
        }
        /*
         else if (nb == 1)
         {
             hit = Physics2D.Raycast(transform.position, -Vector2.up + Vector2.right, maxDist);
             if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
                 tmp = hit.point;
         }

         // up and down
         RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, maxDist);
         if (hit.collider != null && hit.collider.tag != "bone")
             tmp = hit.point;
         hit = Physics2D.Raycast(transform.position, Vector2.up, maxDist);
         if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
             tmp = hit.point;
         // right side
         if (rightSide == false)
         {
             hit = Physics2D.Raycast(transform.position, Vector2.right, maxDist);
             if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
                 tmp = hit.point;
             hit = Physics2D.Raycast(transform.position, Vector2.up + Vector2.right, 5);
             if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
                 tmp = hit.point;
             hit = Physics2D.Raycast(transform.position, -Vector2.up + Vector2.right, maxDist);
             if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
                 tmp = hit.point;
         }
         // left side
         if (rightSide == false)
         {
             hit = Physics2D.Raycast(transform.position, -Vector2.right, maxDist);
             if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
                 tmp = hit.point;
             hit = Physics2D.Raycast(transform.position, -Vector2.right + -Vector2.up, maxDist);
             if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
                 tmp = hit.point;
             hit = Physics2D.Raycast(transform.position, -Vector2.right + Vector2.up, maxDist);
             if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
                 tmp = hit.point;
         }*/
        return (tmp);
    }

    void Raycast()
    {
        Vector2 tmp = new Vector2(0, -5);
        Vector2 tmp2 = new Vector2(0, -5);
        Vector2 closestPos;

        if (isMoving == false)
            return;
        closestPos = getClosestPos();
        if (timer <= Time.time && Vector2.Distance(transform.position, closestPos) < maxDist)
        {
            hitpos = closestPos;
            nb++;
            timer = Time.time + 0.8f;
            if (nb >= follow.Count)
                nb = 0;
        }
        tmp2.x = hitpos.x - playerBis.transform.position.x;
        tmp2.y = hitpos.y - playerBis.transform.position.y;
        tmp.x = closestPos.x - transform.position.x;
        tmp.y = closestPos.y - transform.position.y;
        if (debug == true)
        {
            Debug.DrawRay(transform.position, tmp);
            Debug.DrawRay(transform.position, tmp2, Color.green);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       /* if (collision.tag == "player")
        {
            Vector2 tmp = new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y);
            x = tmp.x;
            y = tmp.y;
            while (x > 1 || y > 1)
            {
                x = x / 2;
                y = y / 2;
            }
        }*/
    }
}
