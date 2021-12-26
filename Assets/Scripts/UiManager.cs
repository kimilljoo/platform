using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    public Image[] Heart;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        scoreText.text = gameManager.score.ToString();
    }

    public void SetHp(int heart, int maxHeart)
    {
        for(int i = 0; i<maxHeart; ++i)
        {
            Heart[i].color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i < heart; ++i)
        {
            Heart[i].color = new Color(1, 1, 1, 1);
        }
    }
}
