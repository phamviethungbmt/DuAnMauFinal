using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScen : MonoBehaviour
{
    
    private void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth =collision.GetComponent<PlayerHealth>();  
            if (playerHealth != null && playerHealth.hadKey == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                PlayerHealth.scoreTemp = PlayerHealth.score;
            }
            
        }
    }
    
}
