using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    [SerializeField] private GameObject bodyBoss;
    Collider2D colliderBodyBoss;
    public static float bossHealth;
    [SerializeField] private GameObject bodyWeakness; //Khai báo một lớp phòng thủ của boss
    void Start()
    {
        bossHealth = 100;
        colliderBodyBoss = bodyBoss.GetComponent<Collider2D>();
        colliderBodyBoss.enabled = false;
        bodyWeakness.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Nếu va chạm đạn của Player thì boss sẽ bị trừ máu
        if (collision.gameObject.tag == "Bullet")
        {
            //Nếu phần đầu của boss dính đạn sẽ bị trừ máu
            Destroy(collision.gameObject);
            if (bodyWeakness.active)
            {
                bossHealth -= 1;
            }
            else
            {
                bossHealth -= 15;
            }
            //nếu máu còn 50 % thì lớp phòng thủ sẽ biến mất
            if (bossHealth <=90)
            {
                colliderBodyBoss.enabled = true;
                bodyWeakness.SetActive(false);
            }
            Debug.Log("Máu còn: " + bossHealth);
            //hết máu thì xóa boss
            if (bossHealth <= 0)
            {
                GameObject boss = GameObject.Find("Boss");
                Destroy(boss);
            }
        }
    }
}
