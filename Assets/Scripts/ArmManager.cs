using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour {

    public int _nbBone;
    public GameObject _bone;
    public List<GameObject> armPrefabs;
    private List<GameObject> _arm = new List<GameObject>();
    public GameObject _following;
    public GameObject _base;

	// Use this for initialization
	void Start () {
        if (armPrefabs.Count == 0)
        {
            for (int i = 0; i != _nbBone; i++)
            {
                _arm.Add(Instantiate(_bone, new Vector2(0, -4.5f), new Quaternion(0, 0, 0, 0)));
                if (i != 0)
                    _arm[i].GetComponent<Follow>()._toFollow = _arm[i - 1];
                else
                    _arm[i].GetComponent<Follow>()._toFollow = _following;
            }
        }
        else
        {
            _arm = armPrefabs;
            for (int i = 0; i != _arm.Count; i++)
            {
                if (i != 0)
                    _arm[i].GetComponent<Follow>()._toFollow = _arm[i - 1];
                else
                    _arm[i].GetComponent<Follow>()._toFollow = _following;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        reposition();

	}

    void reposition()
    {
        for (int i = 0; i != _arm.Count; i++)
        {
            _arm[i].GetComponent<Follow>().MyUpdate();
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
