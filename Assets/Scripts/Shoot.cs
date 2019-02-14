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

    void Start()
    {
        Destroy(this.gameObject, 1);
    }

    void Update()
    {
        Vector2 lookAtThis = target.transform.position - transform.position;

        lookAtThis.Normalize();
        float rot_z = Mathf.Atan2(lookAtThis.y, lookAtThis.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        transform.Translate(Vector2.up * 20 * Time.deltaTime);
    }
}
