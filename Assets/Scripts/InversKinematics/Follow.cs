using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Follow : MonoBehaviour {

    /*
    ** This script is some nerd math I copied from stackoverflow, it calculate the angle of the the limb
    */

    public GameObject _base;
    public GameObject _joint;
    public GameObject _toFollow;
    public float speed;
    public bool findGround = false;
    public int index = 0;
    public bool conraint;
    public int minc;
    public int maxc;

    private SpriteRenderer _spr;
    private Vector2 target;
    private double angle = 0f;
    private float size;

    /*
    ** this function get the sprite of the limb 
    */ 
       
    void Start () {
        _spr = GetComponent<SpriteRenderer>();
        size = Vector2.Distance(_joint.transform.position, _base.transform.position);
    }
	
    /*
    ** this function is called form ArmManager (another script), it recalculate the angle and position of the limb 
    */

	public void MyUpdate (GameObject prevBone) {
       if (_toFollow.tag != "bone" && findGround == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f);
            if (hit.collider != null && hit.collider.gameObject.tag != "player")
                _toFollow.transform.position = hit.point;
        }
        target = _toFollow.transform.position;
        FollowTarget(prevBone);
    }

    /*
    ** this function is called form MyUpdate, it recalculate the angle of the limb 
    */

    void FollowTarget(GameObject prevBone)
    {
        Vector3 dir = new Vector3(0,0);
        double offset = 0;

        dir.x = target.x - _base.transform.position.x;
        dir.y = target.y - _base.transform.position.y;
        angle = Math.Atan2(dir.y, dir.x);
        angle = angle * 180 / 3.14159265;
        if (conraint == true && (angle < offset + minc || angle > offset + maxc))
        {
            if (angle > 90)
                angle = -180;
            else
                angle = 0;
        }
        angle -= 360;
        transform.eulerAngles = new Vector3(0, 0, (float)angle);
        dir = SetMag(size, dir);
        transform.position = new Vector3(target.x - dir.x, target.y - dir.y, 1);
    }

    /*
    ** this fucntion does Math stuff I don't really undrstand but if I don't do this, the angles are wrongs
    */

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

    /*
    ** this fucntion gets the calculated angle of the limb 
    */

    public double GetAngle()
    {
        return (angle);
    }

    /*
    ** this fucntion handle his destruction to remove itself form a list in ArmManager
    */

    private void OnDestroy()
    {
        transform.parent.GetComponent<ArmManager>().RemoveArm(this.gameObject);
    }
}
