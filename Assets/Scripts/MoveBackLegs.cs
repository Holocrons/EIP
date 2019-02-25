using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackLegs : MonoBehaviour
{
    private float timer = 0;
    public GameObject playerBis;
    private Vector3 hitpos;
    private int nb = 0;
    public float maxDist;
    public List<GameObject> follow;
    public bool debug = false;
    bool isMoving = false;
    private float x;
    public float speed;

    /*
   ** this is a test script, it's ugly don't stare at it or you will lose your sanity
   ** It's not used anymore but might be usefull for later 
   ** /!\ it's not commanted /!\
   */

    // Start is called before the first frame update
    void Start()
    {
        hitpos = new Vector3(-1000, -1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        else
            x = 0;
        if (x != 0)
            isMoving = true;
        else
            isMoving = false;
        Raycast();
        if (hitpos != new Vector3(-1000, -1000))
            follow[nb].transform.position = Vector2.MoveTowards(follow[nb].transform.position, hitpos, speed * Time.deltaTime * 8f);
    }

    Vector2 getClosestPos()
    {
        Vector2 tmp = new Vector3(-1000, -1000);
        RaycastHit2D hit;


        if (nb == 0)
        {
            if (x > 0)
            {
                hit = Physics2D.Raycast(transform.position, -Vector2.up, maxDist);
                if (hit.collider != null && hit.collider.tag != "bone")
                    tmp = hit.point;
            }
            else
            {
                hit = Physics2D.Raycast(new Vector2(transform.position.x - 10, transform.position.y), -Vector2.up, maxDist);
                if (hit.collider != null && hit.collider.tag != "bone")
                    tmp = hit.point;
            }
        }
        else if (nb == 1)
        {
            if (x > 0)
            {
                hit = Physics2D.Raycast(new Vector2(transform.position.x + 10, transform.position.y), -Vector2.up, maxDist);
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
            timer = Time.time + 1f;
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
}
