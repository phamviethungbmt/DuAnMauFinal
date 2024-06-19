using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    public int addHeal = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null )
        {
            playerHealth.GetHeal(addHeal);
            Destroy(gameObject);
        }
    }
}
