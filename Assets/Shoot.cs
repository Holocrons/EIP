using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject player;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lol = player.transform.position - transform.position;

        lol.Normalize();
        float rot_z = Mathf.Atan2(lol.y, lol.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        transform.Translate(Vector2.up * 20 * Time.deltaTime);
    }
}
