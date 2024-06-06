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
    void Start()
    {
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
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
        }


        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (!panel.activeSelf)
            {
                Pause();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

    public void Replay()
    {
        SceneManager.LoadScene("Game");
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
        SceneManager.LoadScene("Menu");
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}
