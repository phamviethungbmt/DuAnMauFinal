using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BodyBoss : MonoBehaviour
{
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject bossBody;
    bool BodyImmu = true;

    public int heathPart = 10;
    public int heathPartCurrent;
    public Slider heathPartSilder;

    public GameObject explosionEffect;
    public GameObject explosionBullet;

    public int scoreAdd = 10;

    private void Start()
    {
       

        heathPartCurrent = heathPart;
        heathPartSilder.maxValue = heathPart;
        heathPartSilder.value = heathPartCurrent;
    }

    private void Update()
    {
        //if (bossBody == null)
        //{
        //    Debug.Log("boss chet");
        //    StartCoroutine(Victory());
        //}    

    }
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
                    PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeScore(scoreAdd);
                    }
                }
            }
        }
    }
    public void updateSliderPart()
    {
        heathPartSilder.value = heathPartCurrent;
    }

    //IEnumerator  Victory()
    //{
    //    yield return new WaitForSeconds(3);

    //    panelVictory.SetActive(true);
    //    PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
    //    if (playerHealth != null )
    //    {
    //        scoreVictory = playerHealth.score;
    //    }

    //    scoreVictoryText.text = "YOUR SCORE    " + scoreVictory;
    //}
}
