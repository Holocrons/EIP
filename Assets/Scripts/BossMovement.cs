using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    /*
    ** This script takes care of the basic movement of the boss and his attacks. /!\ This is not his AI /!\
    */

    public float maxDist;
    public float speed = 5;
    public GameObject playerBis;
    public List<GameObject> follow;
    public bool debug = false;
    public List<GameObject> Arms;

    private float x = 0;
    private float y = 0;
    private bool isMoving = false;
    private int nb = 0;
    private float timer = 0;
    private Vector3 hitpos;
    private float maxHight;
    private float minHight;
    private int nbLimbs = 4;
    private ArmManager firstLimb;
    private ArmManager secondLimb;
    private RaycastHit2D groundHit;


    /*
    **  Variables used for the circular attack
    */

    public Transform rotationCenter;
    private bool going = false;
    private float angularSpeed = 5;
    private float attackRadus = 10;
    private float angle = 0;

    /*
    **  Variable used for the smashing attack
    **  https://ih1.redbubble.net/image.10243997.9849/fc,550x550,white.jpg 
    */

    private bool smashing = false;

    /*
    **  Variables used for the throwing attack
    */

    public GameObject projectiles;
    public GameObject player;
    public GameObject attackFollow;
    private bool shooting = false;
    private float timerShoot = 0;
    private int shootCount = 0;

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

    void Update()
    {
        nbLimbs = firstLimb.armPrefabs.Count + secondLimb.armPrefabs.Count;
        Raycast();
        Movement();
        AttackManager();
        if (hitpos != new Vector3(-1000, -1000))
            follow[nb].transform.position = Vector2.MoveTowards(follow[nb].transform.position, hitpos, speed * Time.deltaTime * 8f);
    }

    void AttackManager()
    {
        if (smashing == true)
            SmashAttack();
        else if (shooting == true)
            ThrowThings();
        else if (going == true)
            CircularAttack();

    }

    void Movement()
    {
        if (nbLimbs <= 3 && nbLimbs > 1)
        {
            maxHight = 5.6f;
            minHight = 5.4f;
        }
        groundHit = Physics2D.Raycast(transform.position, -Vector2.up, maxDist);
        if (groundHit.collider != null && Vector2.Distance(transform.position, groundHit.transform.position) < minHight)
            y = 1;
        else if (groundHit.collider != null && Vector2.Distance(transform.position, groundHit.transform.position) > maxHight)
            y = -1;
        else
            y = 0;
        if (x == 0)
            isMoving = false;
        else
            isMoving = true;
        if (smashing == false)
            transform.Translate(new Vector2(x, y) * speed * Time.deltaTime);
    }

    public void Move(int direction)
    {
        if (direction > 0)
            x = 1;
        else if (direction < 0)
            x = -1;
        else
            x = 0;
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

    /*
    **  All boss attacks are coded here
    */ 

    public void ThrowThings()
    {
        GameObject tmp;
        if (timerShoot < Time.time)
        {
            tmp = Instantiate(projectiles, transform.position, transform.rotation);
            tmp.GetComponent<Shoot>().parent = this.gameObject;
            tmp.GetComponent<Shoot>().target = player;
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

    public void SmashAttack()
    {
        smashing = true;
        transform.Translate(new Vector2(0, -1) * speed * 6 * Time.deltaTime);
        if (groundHit.collider != null && groundHit.collider.tag != "player" && Vector2.Distance(transform.position, groundHit.transform.position) < 2)
        {
            smashing = false;
        }
    }

    public void CircularAttack()
    {
        if (going == false)
        {
            attackFollow.transform.position = new Vector2(transform.position.x, transform.position.y + 15);
            going = true;
        }
        float posx = rotationCenter.position.x + Mathf.Cos(angle) * attackRadus;
        float posy = rotationCenter.position.y + Mathf.Sin(angle) * attackRadus;
        attackFollow.transform.position = new Vector2(posx, posy);
        angle = angle + Time.deltaTime * angularSpeed;
        if (angle >= 8)
        {
            attackFollow.transform.position = transform.position;
            going = false;
            angle = 0;
        }
    }
}
