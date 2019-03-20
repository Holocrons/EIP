using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIa : MonoBehaviour
{
    /*
    ** This script is the boss AI it tells him how and when to attack 
    ** (it's more a pathern then an AI but ¯\_(ツ)_/¯)
    */

    public GameObject Target;

    private BossMovement bm;
    private float attackTimer;
    private float coolDown = 1f;
    private float distance;

    /*
    ** this function sets the timer  and get the BossMovvement script 
    */ 

    void Start()
    {
        attackTimer = coolDown + Time.time;
        bm = GetComponent<BossMovement>();
    }

    /*
    ** this function updtaes the Movement and the Attack
    */

    void Update()
    {
        Movement();
        AttackManager();
    }

    /*
    ** this function makes the boss attack the player depending on the distance between them and the attack timer (advanced ai here)
    */

    void AttackManager()
    {
        int fixedtime = 0;

        if (attackTimer <= Time.time)
        {
            if (distance < 9 && distance > 6.5f)
            {
                bm.CircularAttack();
                fixedtime = 2;
            }
            else if (distance < 6.5 && Target.transform.position.y < transform.position.y)
                bm.SmashAttack();
            else
                bm.ThrowThings();
            RestartTimer(fixedtime);
        }
    }

    /*
    ** this function moves the boss toward the player
    */

    void Movement()
    {
        distance = Vector2.Distance(Target.transform.position, transform.position);

        if (distance > 7.5)
        {
            if (Target.transform.position.x > transform.position.x)
                bm.Move(1);
            else if (Target.transform.position.x < transform.position.x)
                bm.Move(-1);
        }
        else
            bm.Move(0);
    }

    /*
    ** this function restarts the timer and sets the coolDown to a random number between 1 and 2.5 
    */

    void RestartTimer(float fixedTime = 0)
    {
        if (fixedTime == 0)
            coolDown = Random.Range(1f, 2.5f);
        else
            coolDown = fixedTime;
        attackTimer = coolDown + Time.time;
    }
}
