using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    // tmp script might delete later
    private int x;
    private int y;

    Vector2 landing;

    // Start is called before the first frame update
    void Start()
    {
        landing = new Vector2(transform.position.x + 10, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.D))
            x = 1;
        else if (Input.GetKey(KeyCode.Q))
            x = -1;
        else
            x = 0;
        if (Input.GetKey(KeyCode.Z))
            y = 1;
        else if (Input.GetKey(KeyCode.S))
            y = -1;
        else
            y = 0;
        transform.Translate(new Vector2(x * 7 * Time.deltaTime, y * 7 * Time.deltaTime));
        Vector2.MoveTowards(transform.position, landing, 5f * Time.deltaTime * 8f);


    }
}
