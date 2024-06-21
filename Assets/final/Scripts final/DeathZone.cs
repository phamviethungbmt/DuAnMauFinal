using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject panelDefeat;

    private void Start()
    {
        panelDefeat.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy (collision.gameObject);
            Time.timeScale = 0;
            panelDefeat.SetActive (true);
            PlayerHealth.score = PlayerHealth.scoreTemp;
        }
    }
    
}
