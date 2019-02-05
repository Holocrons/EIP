using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    private Vector3 _target;
	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _target.z = 1;
        _target.x += 0.1f;
        _target.y -= 0.1f;
        transform.position = _target;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "target" && Input.GetMouseButton(0))
        {
            collision.transform.position = transform.position;
        }
    }
}
