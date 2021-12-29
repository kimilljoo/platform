using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private int stageNum = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ++stageNum;
            Debug.Log(stageNum);
            if(stageNum == 3)
            {

                SceneManager.LoadScene("GameOverScene");
            }
            else
            {
                SceneManager.LoadScene("Stage" + stageNum.ToString());
            }
        }
    }
}
