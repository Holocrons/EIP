using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviour
{
    /*
    ** Just the health manager of the limb, nothing incredible 
    ** hp is his health points
    ** recoveryTimer is the timer used for the invicibiliy (see OnTriggerEnter2D)
    */ 

    public int hp = 20;

    private float recoveryTimer = 0;

    /*
    ** Destroys the limb when hp is equale to 0 
    */

    void Update()
    {
        if (hp <= 0)
            Destroy(this.gameObject);
    }

    /*
    ** When the limb touch something which have the tag "damage", the limb loses 1 hp and become invicible for 0.5 second 
    */ 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "dommage" && recoveryTimer <= Time.time)
        {
            recoveryTimer = Time.time + 0.5f;
            hp--;
        }
    }
}
