using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIa : MonoBehaviour
{

    /*
    ** 
    ** This script is the boss AI it tells him how and when to attack 
    ** (it's more a pathern then an AI but ¯\_(ツ)_/¯)
    **
    */

    private BossMovement bm;
    private float attackTimer;
    private float coolDown = 1f;
    private float distance;

    public GameObject Target;

    void Start()
    {
        attackTimer = coolDown + Time.time;
        bm = GetComponent<BossMovement>();
    }

    void Update()
    {
        Movement();
        AttackManager();
    }


    void AttackManager()
    {
        int fixedtime = 0;

        if (attackTimer <= Time.time)
        {
            if (distance < 8 && distance > 6)
            {
                bm.CircularAttack();
                fixedtime = 2;
            }
            else if (distance > 8)
                bm.ThrowThings();
            else
                bm.SmashAttack();
            RestartTimer(fixedtime);
        }
    }

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

    void RestartTimer(float fixedTime = 0)
    {
        if (fixedTime == 0)
            coolDown = Random.Range(0.5f, 2f);
        else
            coolDown = fixedTime;
        attackTimer = coolDown + Time.time;
    }
}
