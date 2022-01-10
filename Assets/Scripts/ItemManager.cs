using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlayerManager player;
    private UiManager uiManager;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UiManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.name == "Coin")
            {
                GameManager.score += 500;
                Debug.Log(GameManager.score);
                Destroy(gameObject);
            }
            else if(gameObject.name == "Star")
            {
                player.isStar = true;
                Destroy(gameObject);
            }
            else if(gameObject.name == "Heart")
            {
                if(GameManager.maxHp == GameManager.curHp)
                {
                    GameManager.score += 500;
                    Destroy(gameObject);

                }
                else
                {
                    ++GameManager.curHp;
                    Debug.Log(GameManager.curHp);
                    uiManager.SetHp();
                    Destroy(gameObject);
                }
            }
        }
    }
}
