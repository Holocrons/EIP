using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] inventory;

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
}