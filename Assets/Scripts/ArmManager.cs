using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour {

    public List<GameObject> armPrefabs;
    private List<GameObject> _arm = new List<GameObject>();
    public GameObject _following;
    public GameObject _base;

	// Use this for initialization
	void Start () {
        _arm = armPrefabs;
        for (int i = 0; i != _arm.Count; i++)
        {
            if (i != 0)
                _arm[i].GetComponent<Follow>()._toFollow = _arm[i - 1];
            else
                _arm[i].GetComponent<Follow>()._toFollow = _following;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (_arm.Count != 0)
            Reposition();
        //RepositionCCD();
	}

    void RepositionCCD()
    {
        for (int i = 0; i != _arm.Count; i++)
        {
            if (i == _arm.Count - 1)
            {
                double ab = Vector2.Distance(_arm[i].GetComponent<Follow>()._joint.transform.position, _following.transform.position);
                double bc = Vector2.Distance(_arm[i].GetComponent<Follow>()._base.transform.position, _arm[i].GetComponent<Follow>()._joint.transform.position);
                double ca = Vector2.Distance(_arm[i].GetComponent<Follow>()._base.transform.position, _following.transform.position);
                double tmp = (Math.Pow(ca, 2) + Math.Pow(ab, 2) - Math.Pow(bc, 2)) / (2 * ca * ab);
                double angle = Math.Acos(tmp);

                if (double.IsNaN(angle))
                    return;
                angle = angle * (180 / Math.PI);
                Debug.Log(angle);
                _arm[i].transform.eulerAngles = new Vector3(0, 0, _arm[i].transform.eulerAngles.z + (float)angle);
            }
        }
    }

    void Reposition()
    {
        for (int i = 0; i != _arm.Count; i++)
        {
            if (i == _arm.Count - 1)
                _arm[i].GetComponent<Follow>().MyUpdate(null);
            else
            {
                _arm[i].GetComponent<Follow>().MyUpdate(_arm[i + 1]);
            }
        }
        _arm[_arm.Count - 1].transform.position = _base.transform.position;
        for (int i = _arm.Count - 2; i >= 0; i--)
        {
            _arm[i].transform.position = _arm[i + 1].GetComponent<Follow>()._joint.transform.position;
        }
    }

    public void RemoveArm(GameObject arm)
    {
        _arm.Remove(arm);
        for (int i = 0; i != _arm.Count; i++)
        {
            if (i != 0)
                _arm[i].GetComponent<Follow>()._toFollow = _arm[i - 1];
            else
                _arm[i].GetComponent<Follow>()._toFollow = _following;
        }
    }
}
