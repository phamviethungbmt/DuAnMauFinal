using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class Victory : MonoBehaviour
{
    public GameObject panelVictory;
    private int scoreVictory;
    public TextMeshProUGUI scoreVictoryText;

    public GameObject bossBody;

    public AudioClip victoryClip;
    public AudioClip backGroundMusic;
    private AudioSource audioSource;
    

    private void Start()
    {
        panelVictory.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backGroundMusic; audioSource.Play();
        audioSource.loop = true;
        // muốn chạy backgroundmusisc chỗ này luc skhowir động sence
        
    }

    private void Update()
    {
        if(bossBody == null)
        {
            StartCoroutine(ShowVictory());
        }
        
    }
    IEnumerator ShowVictory()
    {
        yield return new WaitForSeconds(3);
        
        
        audioSource.Stop();

        audioSource.PlayOneShot(victoryClip);

        panelVictory.SetActive(true);
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            scoreVictory = PlayerHealth.score;
        }

        scoreVictoryText.text = "YOUR SCORE    " + scoreVictory;
    }
}
