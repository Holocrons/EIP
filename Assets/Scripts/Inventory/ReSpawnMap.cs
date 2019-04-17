using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawnMap : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        Debug.Log("DROP");
        Vector2 playerPos = new Vector2(player.position.x + 2, player.position.y);

        //Instantiate(item, playerPos, Quaternion.identity);

        item.gameObject.SetActive(true);
    }
}
