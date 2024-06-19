using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int trapDamage = 1;
    //hung
    //Hàm va chạm vào trap sẽ làm cho nhân vật dính 1 damage và bị trừ 1 máu
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(trapDamage);
        }
    }
   
}
