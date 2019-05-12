using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkManager : MonoBehaviour
{
    public List<GameObject> limbList;
    public GameObject target;
    public GameObject effector;
    private int f;

    void Start()    
    {
        f = limbList.Count - 1;
        limbList[f].GetComponent<LimbManager>().canBeHit = true;
    }

    void Update()
    {
        if (effector != null && Vector2.Distance(target.transform.position, effector.transform.position) > 0.25f)
        {
            if (f > limbList.Count - 1)
                f = limbList.Count - 1;
            limbList[f].GetComponent<LookAtTarget>().Turn();
                f--;
            if (f < 0)
                f = limbList.Count - 1;
        }
        else
            f = limbList.Count - 1;
    }

    void MoveLimbs()
    {
        int i = limbList.Count - 1;
        while (i >= 0)
        {
            if (Vector2.Distance(target.transform.position, effector.transform.position) < 1)
            {
                Debug.Log("returned");
                return;
            }
            limbList[i].GetComponent<LookAtTarget>().Turn();
            i--;
        }
    }

    public void NewEffector()
    {
        limbList.RemoveAt(limbList.Count - 1);
        if (limbList.Count == 0)
        {
            effector = null;
            return;
        }
        limbList[limbList.Count - 1].GetComponent<LimbManager>().canBeHit = true;
        effector = limbList[limbList.Count - 1].GetComponent<LookAtTarget>()._joint;
        foreach (GameObject limb in limbList)
        {
            limb.GetComponent<LookAtTarget>()._effector = effector;
        }
    }
}
