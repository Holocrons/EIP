using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawnMap : MonoBehaviour
{
    //public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem(GameObject inventory)
    {
        Debug.Log("DROP");
        Vector2 playerPos = new Vector2(player.position.x + 2, player.position.y);


        Debug.Log(playerPos);
        //DestroyImmediate(itemButton, true);
        
        //Instantiate(this.gameObject, playerPos, Quaternion.identity);
        //inventory.SetActive(true);
        //item.gameObject.SetActive(true);
        //itemButton.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
        this.transform.position = playerPos;
        //Instantiate(inventory.gameObject, playerPos, Quaternion.identity);
    }
}
