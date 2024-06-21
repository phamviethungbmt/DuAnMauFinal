using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    public GameObject player;
    private bool playerDeath = false;

    public GameObject panelDefeat;

    private void Start()
    {
        panelDefeat.SetActive(false);
    }
    private void Update()
    {
        CheckPlayerDeath();
        if(playerDeath == true)
        {
            ShowDefeat();
        }
    }
    public void CheckPlayerDeath()
    {
        if (player == null)
        {
            playerDeath = true;
        }

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null )
        {
            if(playerHealth.currentHealth == 0 )
            {
                playerDeath = true;
            }
        }

        
    }

    public void ShowDefeat()
    {
        Time.timeScale = 0f;
        panelDefeat.SetActive (true);

    }
}
