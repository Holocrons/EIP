using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkManager : MonoBehaviour
{
    public List<GameObject> limbList;

    private int i;

    // Start is called before the first frame update
    void Start()
    {
        i = limbList.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            limbList[i].GetComponent<LookAtTarget>().Turn();
            i--;
            if (i < 0)
                i = limbList.Count - 1;
        }
    }
}
