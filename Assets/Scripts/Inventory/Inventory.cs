using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] inventory;
    public int inventoryIterator = 0;

    private Button myButton;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Object") && Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i<slots.Length; i++)
            {
                if (isFull[i] == false)
                {
                    // ITEM CAN BE ADDED TO INVENTORY
                    isFull[i] = true;
                    inventory[i] = other.gameObject; 
                    Instantiate(other.gameObject.GetComponent<PickUp>().itemButton, slots[i].transform, false);
                    other.gameObject.SetActive(false);
                    Debug.Log("courge" + i.ToString());
                    Debug.Log(slots[i].name);
                    break;
                }
            }
        }
    }

    void Start()
    {
        slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 0);
            if (inventoryIterator >= 9)
                inventoryIterator = 0;
            else
                inventoryIterator++;
            slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 255);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 0 );
            if (inventoryIterator <= 0)
                inventoryIterator = 9;
            else
                inventoryIterator--;
            slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 255);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 0);
            if (inventoryIterator >= 8)
                inventoryIterator = (inventoryIterator + 2) % 10;
            else
                inventoryIterator+= 2;
            slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 255);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 0);
            if (inventoryIterator <= 1)
                inventoryIterator += 8;
            else
                inventoryIterator -= 2;
            slots[inventoryIterator].gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255, 255);
        }

        Debug.Log("inventoryIterator: " + inventoryIterator);
    }
}