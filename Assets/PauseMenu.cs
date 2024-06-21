using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public Slider volumeSlider;

    private void Start()
    {
        PausePanel.SetActive(false);
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {

        PlayerHealth.score = PlayerHealth.scoreTemp;
        Time.timeScale = 1f;
        Scene currenScence = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currenScence.name);
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PausePanel.activeSelf)
            {
                Time.timeScale = 0f;
                PausePanel.SetActive(true);

            }
            else
            {
                Time.timeScale = 1f;
                PausePanel.SetActive(false);
            }
        }
    }
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit(); khi build game
    }
}
