using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageBody : MonoBehaviour
{
    [SerializeField] private GameObject bodyBoss;
    Collider2D colliderBodyBoss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Nếu phần thân của boss dính đạn sẽ bị trừ máu gấp đôi
            Destroy(collision.gameObject);
            DamageBoss.bossHealth -= 10;
            Debug.Log("Máu còn: " + DamageBoss.bossHealth);
            if (DamageBoss.bossHealth <= 0)
            {
                GameObject boss = GameObject.Find("Boss");
                Destroy(boss);
            }
        }
    }
}
