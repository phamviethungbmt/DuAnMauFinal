using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public GameObject explosionEffect;
    public int bulletDamage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null )
        {
            playerHealth.TakeDamage(bulletDamage);
            Destroy(gameObject);
        } 
        
        if (collision.CompareTag("ground") || collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject a = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            //Xóa vụ nổ
            Destroy(a, 0.9f);
        }
    }
}
