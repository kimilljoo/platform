using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject UIPanel;

    public int PanelSwitchNum;

    public Image[] Heart;
    
    public Toggle toggle;

    private void Update()
    {
        DrawText();
        SwitchPanel();
        if(toggle != null)
        {
            ToggleAudio();

        }
    }

    public void SwitchPanel()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ++PanelSwitchNum;
            if(PanelSwitchNum %2 == 0)
            {
                Time.timeScale = 1;
                UIPanel.SetActive(false);
            }
            else if(PanelSwitchNum %2 == 1)
            {
                Time.timeScale = 0;

                UIPanel.SetActive(true);

            }
        }
    }

    public void ExitPanel()
    {
        ++PanelSwitchNum;
        Time.timeScale = 1;
        UIPanel.SetActive(false);
    }

    public void DrawText()
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

    public void ToggleAudio()
    {
        AudioListener.volume = toggle.isOn == true ? 1 : 0;
    }

}
