using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MenuPanel;
    public Slider volumeSilder;
    private bool isPause = false;
    public GameObject PanelSetting;
    void Start()
    {
        MenuPanel.SetActive(false);
        PanelSetting.SetActive(false);
        if (volumeSilder != null)
        {
            volumeSilder.value = AudioListener.volume;
            volumeSilder.onValueChanged.AddListener(ChangeVolume);
        }
    }

    void Update()
    {
        

        //if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    if (!panel.activeSelf)
        //    {
        //        Pause();
        //    }
        //    else
        //    {
        //        ResumeGame();
        //    }
        //}
    }
    public void Game()
    {
        PlayerHealth.score = 0;
        PlayerHealth.scoreTemp = 0;
        SceneManager.LoadScene(1);
    }

 

    public void Setting()
    {
        PanelSetting.SetActive(true);
    }

    public void OutSetting()
    {
        PanelSetting.SetActive(false);
    }

    public void Menu2()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void Pause()
    {
        if (!isPause)
        {
            Time.timeScale = 0f;
            isPause = true;
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPause = false;
    }
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit(); khi build game
    }

    public void ClickToPlay()
    {
        MenuPanel.gameObject.SetActive(true);
    }
}
