using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

    private float x = 0;
    private float y = 0;
    public float maxDist;
    bool isMoving = false;
    private int nb = 0;
    public float speed = 5;
    public GameObject cam;
    private float timer = 0;
    public GameObject playerBis;
    private float timerRay = 0;
    private bool sw = true;
    private Vector2 hitpos = new Vector2(0, 0);
    public List<GameObject> follow;
    public bool debug = false;
    public List<GameObject> limbs;
    public GameObject parent;
    public GameObject otherPlayer = null;
    private int running = 1;

    // Use this for initialization
    void Start () {
        playerBis.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKey(KeyCode.D))
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
         if (Input.GetKey(KeyCode.W))
         {
             y = 1;
         }
         else if (Input.GetKey(KeyCode.S))
         {
             y = -1;
         }
         else
         {
             y = 0;
         }
         */
        if (x == 0 && y == 0)
            isMoving = false;
        else
            isMoving = true;
        if (otherPlayer != null)
        {
            Vector2 vec = new Vector2(playerBis.transform.position.x + x * 3f, playerBis.transform.position.y + y * 3f);
            otherPlayer.transform.position = vec;
        }
        transform.Translate(new Vector2(x, y) * speed * running * Time.deltaTime);
        playerBis.transform.position = new Vector2(transform.position.x - x, transform.position.y);
        Raycast();
        follow[nb].transform.position = Vector2.MoveTowards(follow[nb].transform.position, hitpos, speed * Time.deltaTime * 4f);
        x = 0;
        y = 0;
        /*if (otherPlayer != null)
          cam.transform.position = new Vector3(transform.position.x, transform.position.y, -15);*/
    }

    Vector2 getClosestPos()
    {
        Vector2 tmp = new Vector2(0, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, maxDist);

        if (hit.collider != null && hit.collider.tag != "bone")
            tmp = hit.point;
        hit = Physics2D.Raycast(transform.position, Vector2.up, maxDist);
        if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
            tmp = hit.point;
        hit = Physics2D.Raycast(transform.position, Vector2.right, maxDist);
        if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
            tmp = hit.point;
        hit = Physics2D.Raycast(transform.position, -Vector2.right, maxDist);
        if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
            tmp = hit.point;
        hit = Physics2D.Raycast(transform.position, Vector2.up + Vector2.right, 5);
        if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
            tmp = hit.point;
        hit = Physics2D.Raycast(transform.position, -Vector2.up + Vector2.right, maxDist);
        if (hit.collider != null && hit.collider.tag != "bone" && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
            tmp = hit.point;
        hit = Physics2D.Raycast(transform.position, -Vector2.right + -Vector2.up, maxDist);
        if (hit.collider != null && hit.collider.tag != "bone"  && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
            tmp = hit.point;
        hit = Physics2D.Raycast(transform.position, -Vector2.right + Vector2.up, maxDist);
        if (hit.collider != null && hit.collider.tag != "bone"  && Vector2.Distance(transform.position, tmp) > Vector2.Distance(transform.position, hit.point))
            tmp = hit.point;
        return (tmp);
    }

    void Raycast()
    {
        Vector2 tmp = new Vector2(0, -5);
        Vector2 tmp2 = new Vector2(0, -5);
        Vector2 tmp3 = new Vector2(0, -5);
        Vector2 closestPos;

        if (isMoving == false)
            return;
        closestPos = getClosestPos();
        if (timer <= Time.time && Vector2.Distance(transform.position, closestPos) < maxDist)
        {
            hitpos = closestPos;
            nb++;
            timer = Time.time + (0.4f );
            if (nb >= follow.Count)
                nb = 0;
        }
        tmp2.x = hitpos.x - playerBis.transform.position.x;
        tmp2.y = hitpos.y - playerBis.transform.position.y;
        tmp.x = closestPos.x - transform.position.x;
        tmp.y = closestPos.y - transform.position.y;
        /* else if (hit.collider == null)
             transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
         if (hit.collider != null && Vector2.Distance(transform.position, hit.point) > 3.6f)
         {
             transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
         }
         else if (hit.collider != null && Vector2.Distance(transform.position, hit.point) < 3.4f)
         {
             transform.Translate(new Vector2(0, 1) * speed * Time.deltaTime);
         }*/
        if (debug == true)
        {
            Debug.DrawRay(transform.position, tmp);
            Debug.DrawRay(transform.position, tmp2, Color.green);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            Vector2 tmp = new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y);
            x = tmp.x;
            y = tmp.y;
            while (x > 1 || y > 1)
            {
                x = x / 2;
                y = y / 2;
            }  
            Debug.Log(x.ToString() + " | " + y.ToString());
        }
    }
}
