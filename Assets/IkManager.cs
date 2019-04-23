using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkManager : MonoBehaviour
{
    public List<GameObject> limbList;
    public GameObject target;
    public GameObject effector;
    private int f;
    private float tmp = 0;

    // Start is called before the first frame update
    void Start()    
    {
        f = limbList.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        //limbList[f].GetComponent<LookAtTarget>().Turn();
        //MoveLimbs();

        if (Vector2.Distance(target.transform.position, effector.transform.position) > 1)
        {
            if (limbList[f].GetComponent<LookAtTarget>().Turn() == true)
            {
                Debug.Log("lol");
                tmp++;
                f--;
            }
            if (f < 0)
                f = limbList.Count - 1;
        }
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
}
