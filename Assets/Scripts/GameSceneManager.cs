using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        SceneManager.LoadScene(0);
        GameManager.score = 0;
    }
    public void QuitButton()
    {
        Application.Quit();
    }    
}
