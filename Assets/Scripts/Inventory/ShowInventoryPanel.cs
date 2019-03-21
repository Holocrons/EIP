using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInventoryPanel : MonoBehaviour
{

    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
