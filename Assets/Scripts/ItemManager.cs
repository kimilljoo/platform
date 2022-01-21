using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private PlayerManager player;
    private UiManager uiManager;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UiManager>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ItemEffect();
        }
    }
    private void ItemEffect()
    {
        switch (gameObject.tag)
        {
            case "Coin":
                GameManager.score += 500;
                Debug.Log(GameManager.score);
                Destroy(gameObject);
                break;

            case "Star":
                GameManager.score += 1500;
                player.isStar = true;
                Destroy(gameObject);
                break;
            case "Heart":
                if (GameManager.maxHp == GameManager.curHp)
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
                break;

        }
    }

}
