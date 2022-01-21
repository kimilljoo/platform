using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    UiManager uiManager;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UiManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                SceneManager.LoadScene(2);
                uiManager.SetHp();
                Debug.Log(GameManager.curHp);
                break;
            case 2:
                SceneManager.LoadScene(3);
                break;

        }
    }

}
