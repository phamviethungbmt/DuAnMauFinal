using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapfall : MonoBehaviour
{
    private bool isDamage = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null && isDamage == false)
            {
                playerHealth.TakeDamage(1);
                isDamage = true;
            
                //Debug.Log(playerHealth.currentHealth);
            }
        
    }

}
