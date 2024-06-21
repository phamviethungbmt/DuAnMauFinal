using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject panel;
    public Slider volumeSilder;
    private bool isPause = false;
    //public GameObject MenuPanel;
    void Start()
    {
        //MenuPanel.SetActive(false);
        panel.SetActive(false);
        if (volumeSilder != null)
        {
            volumeSilder.value = AudioListener.volume;
            volumeSilder.onValueChanged.AddListener(ChangeVolume);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!panel.activeSelf)
            {
                Time.timeScale = 0f;
                panel.SetActive(true);
                
            }
            else
            {
                Time.timeScale = 1f;
                panel.SetActive(false);
            }
        }


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

    public void Replay()
    {
        
        PlayerHealth.score = PlayerHealth.scoreTemp;
        Time.timeScale = 1f;
        Scene currenScence = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currenScence.name);
    }

    public void Setting()
    {
        panel.SetActive(true);
    }

    public void OutSetting()
    {

        panel.SetActive(false);
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
        //MenuPanel.gameObject.SetActive(true);
    }

}
