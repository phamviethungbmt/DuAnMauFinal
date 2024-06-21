using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public GameObject explosionBullet;
    public int damageEnemy = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Vector3 posExplosive = transform.position;

            GameObject a = Instantiate(explosionBullet, posExplosive, Quaternion.identity);
            Destroy(a, 0.9f);
            Destroy(gameObject);
        }
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageEnemy);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("vacham voi player");
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageEnemy);
                Destroy(gameObject);
            }
        }
        
    }

   
}
