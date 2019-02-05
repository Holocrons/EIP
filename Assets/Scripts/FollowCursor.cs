using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

    private Vector3 _target;
    public int _mouse;

    // Use this for initialization
	void Start () {
        _target = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
       /* if (Input.GetMouseButton(_mouse))
         {
             _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             _target.z = 1;
         }
        transform.position = _target;*/
    }

    public void setTarget(Vector3 target)
    {
        _target = target;
    }
}
