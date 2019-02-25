using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;

    int x = 0;
    float range = 5f;
    private bool isFacingRight = false;

    public Transform target;

    void Start ()
    {
       // target = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>(); 
    }

    void Update()
    {
        /* if (Vector2.Distance(transform.position, target.position) > 3)
         {
             float stock_y = transform.position.y;
             transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
             transform.position = new Vector2(transform.position.x, stock_y);
         }*/

        if (target.position.x > transform.position.x)
        {
            x = 1;
        } else
        {
           x = -1;
        }
        flipSprite(x);
        enemyAggro(x);
    }

    void flipSprite(int x)
    {
        if ((x > 0 && !isFacingRight) || (x < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector2 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    void enemyAggro(int x)
    {
        if (Vector2.Distance(transform.position, target.position) <= range)
        {
            if (Vector2.Distance(transform.position, target.position) > 1)
            {
                transform.Translate(new Vector2(x, 0) * speed * Time.deltaTime);
            }
        }
    }
}    




