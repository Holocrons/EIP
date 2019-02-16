using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    // tmp script might delete later
    public int x;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
            x = 1;
        else if (Input.GetKey(KeyCode.A))
            x = -1;
        else
            x = 0;
        transform.Translate(new Vector2(x * 7 * Time.deltaTime, 0));
    }
}
