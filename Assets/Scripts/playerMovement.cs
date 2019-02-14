using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    int x = 0;
    int y = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1;
        }
        else
        {
            x = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            y = -1;
        }
        else
        {
            y = 0;
        }
        transform.Translate(new Vector2(x, y) * 5 * Time.deltaTime);
    }
}
