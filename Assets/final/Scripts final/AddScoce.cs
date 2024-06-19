using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoce : MonoBehaviour
{
    public int scoreAdd;

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeScore(scoreAdd);
            Destroy(gameObject);
        }
        
    }
}
