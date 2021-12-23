using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlayerManager player;
    private GameManager gameManager;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.name == "Coin")
            {
                gameManager.score += 500;
                Debug.Log(gameManager.score);
                Destroy(gameObject);
            }
            else if(gameObject.name == "Heart")
            {
                ++player.curHp;
                Debug.Log(player.curHp);
                Destroy(gameObject);
            }
        }
    }
}
