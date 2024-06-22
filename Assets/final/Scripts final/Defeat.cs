using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    public GameObject player;
    public bool playerDeath = false;

    public GameObject panelDefeat;

    private void Start()
    {
        playerDeath = false;
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
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null )
        {
            if(playerHealth.currentHealth <= 0 || player == null)
            {
                playerDeath = true;
            }
            
        }
    }

    public void ShowDefeat()
    {
        Time.timeScale = 0;
        panelDefeat.SetActive (true);

    }
}
