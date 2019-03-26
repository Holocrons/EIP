using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    /*
    ** This script shoot itself from the parent to the target 
    */

    public GameObject target;
    public GameObject parent;

    private bool left = true;


    /*
    ** the projectile will last 1 second 
    */
    
    void Start()
    {
        if (target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-0.75f, 0.75f);
            left = false;
        }
        Vector2 lookAtThis = target.transform.position - transform.position;
        lookAtThis.Normalize();
        float rot_z = Mathf.Atan2(lookAtThis.y, lookAtThis.x) * Mathf.Rad2Deg;
        if (left)
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        Destroy(this.gameObject, 1);
    }

    /*
    ** each frame the projectiles will aims for the target and follows it 
    */ 

    void Update()
    {
        
        if (left)
            transform.Translate(-Vector2.right * 20 * Time.deltaTime);
        else
            transform.Translate(Vector2.right * 20 * Time.deltaTime);
    }

    /*
    ** Do things when the projectile touch something 
    */ 

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
