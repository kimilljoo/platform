using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    public Image[] Heart;


    private void Start()
    {
    }

    private void Update()
    {
        scoreText.text = GameManager.score.ToString();
    }

    public void SetHp()
    {
        for(int i = 0; i<GameManager.maxHp; ++i)
        {
            Heart[i].color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i < GameManager.curHp; ++i)
        {
            Heart[i].color = new Color(1, 1, 1, 1);
        }
    }
}
