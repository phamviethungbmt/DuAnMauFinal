using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyBoss : MonoBehaviour
{
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    bool BodyImmu = true;

    public int heathPart = 10;
    public int heathPartCurrent;
    public Slider heathPartSilder;

    public GameObject explosionEffect;
    public GameObject explosionBullet;
    private void Start()
    {
        heathPartCurrent = heathPart;
        heathPartSilder.maxValue = heathPart;
        heathPartSilder.value = heathPartCurrent;
    }

    //private void Update()
    //{
    //    checkDestroy();

    //}
    public void checkDestroy()
    {
        if (part1 == null && part2 == null && part3 == null && part4 == null)
        {
            BodyImmu = false;
        }
        else
        {
            BodyImmu = true; 
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Vector3 posExplosive = collision.transform.position;

            GameObject a = Instantiate(explosionBullet, posExplosive, Quaternion.identity);
            Destroy(a, 0.9f);
            Destroy(collision.gameObject);

            checkDestroy();

            if (BodyImmu == false)
            {
                heathPartCurrent -= 1;
                updateSliderPart();

                if (heathPartCurrent <= 0)
                {
                    Destroy(gameObject); // Tiêu diệt BodyBoss khi hết máu
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    // Các hành động khác khi tiêu diệt BodyBoss
                }
            }
        }
    }
    public void updateSliderPart()
    {
        heathPartSilder.value = heathPartCurrent;
    }
}
