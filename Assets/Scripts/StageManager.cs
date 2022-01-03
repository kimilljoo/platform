using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            case 1:
                SceneManager.LoadScene(2);
                break;

        }
    }
}
