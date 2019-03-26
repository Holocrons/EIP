using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinates : MonoBehaviour {

    public float x = 0;
    public float y = 0;

    private void OnValidate()
    {
        this.gameObject.name = x + "x" + y;
        Vector2 newPos = new Vector2(x * 3.75f, y * 3.75f);
        Debug.Log(newPos);
        transform.position = newPos;
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
