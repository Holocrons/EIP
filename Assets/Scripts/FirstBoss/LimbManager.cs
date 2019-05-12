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
    public bool canBeHit = false;
    public GameObject par;

    private bool vul = true;
    private float recoveryTimer = 0;
    private SpriteRenderer sp;
    private float blinkTimer = 0;
    private bool isOk = false;
    

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }


    /*
    ** Destroys the limb when hp is equale to 0 
    */

    void Update()
    {
        if (hp <= 0)
        {
            par.GetComponent<IkManager>().NewEffector();
            Destroy(this.gameObject);
        }
        if (vul == false && recoveryTimer <= Time.time)
            vul = true;
        if (vul == false)
            Blink();
    }

    /*
    ** When the limb touch something which have the tag "damage", the limb loses 1 hp and become invicible for 0.5 second 
    */ 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeHit == true && collision.tag == "dommage" && vul == true)
        {
            recoveryTimer = Time.time + 0.5f;
            blinkTimer = Time.time + 0.1f;
            hp--;
            vul = false;
        }
    }

    void Blink()
    {
        if (blinkTimer <= Time.time)
        {
            sp.enabled = isOk;
            blinkTimer = Time.time + 0.1f;
            isOk = !isOk;
        }
    }
}
