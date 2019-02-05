using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletdis : MonoBehaviour {

    public float timer;
	// Use this for initialization
	void Start () {
        timer += Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer <= Time.time)
        {
            Destroy(gameObject);
        }
	}
}
