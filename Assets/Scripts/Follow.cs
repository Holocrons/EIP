using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Follow : MonoBehaviour {

    public GameObject _base;
    public GameObject _joint;
    public float speed;
    private SpriteRenderer _spr;
    public GameObject _toFollow;
    private Vector2 target;
    private double angle = 0f;
    private Vector2 targetPos = new Vector2(0, 0);
    public bool findGround = false;
    public int index = 0;

    // Use this for initialization
    void Start () {
        _spr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	public void MyUpdate () {
       if (_toFollow.tag != "bone" && findGround == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f);
            if (hit.collider != null && hit.collider.gameObject.tag != "player")
            {
                _toFollow.transform.position = hit.point;
            }
            Debug.Log(hit.collider.name);
        }
        target = _toFollow.transform.position;
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 dir = new Vector3(0,0);

        dir.x = target.x - _base.transform.position.x;
        dir.y = target.y - _base.transform.position.y;
        angle = Math.Atan2(dir.y, dir.x);
        angle = angle * 180 / 3.14159265;
        angle -= 360;
        transform.eulerAngles = new Vector3(0, 0, (float)angle);
        dir = SetMag(_spr.size.x * transform.localScale.x, dir);
        transform.position = new Vector3(target.x - dir.x, target.y - dir.y, 1);
    }

    Vector2 SetMag(float n, Vector2 vec)
    {
        float m = (float)Math.Sqrt(vec.x * vec.x + vec.y * vec.y);
        if (m != 0 && m != 1)
        {
            vec.x /= m;
            vec.y /= m;
        }
        vec.x *= n;
        vec.y *= n;
        return vec;
    }
}
