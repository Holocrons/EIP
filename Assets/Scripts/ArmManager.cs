using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour {

    /*
    ** This script manage his arm so each bone will alaways follow ToFallow or the next bone 
    */

    public List<GameObject> armPrefabs;
    public GameObject _following;
    public GameObject _base;

    private List<GameObject> _arm = new List<GameObject>();

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
	
	void Update () {
        if (_arm.Count != 0)
            Reposition();
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
            _arm[i].transform.position = _arm[i + 1].GetComponent<Follow>()._joint.transform.position;
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
