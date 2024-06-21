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
    [SerializeField] private GameObject lazer;
    [SerializeField] private Transform posLazerCenter,posLazerLeft,posLazerRight;
    float shootSpeed = 1;
    [SerializeField] private float lazerSpeed;
    float time;

    private void Start()
    {

        time = 0;
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
        if (time >= shootSpeed)
        {
            StartCoroutine(ShootLazer());
            time = 0; // Reset thời gian
        }
        time += Time.deltaTime;
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
    IEnumerator ShootLazer()
    {
        // Bắn lazer từ ba vị trí
        GameObject centerLAzer = Instantiate(lazer, posLazerCenter.position, Quaternion.Euler(0,0,-90));
        Rigidbody2D rb1 = centerLAzer.GetComponent<Rigidbody2D>();
        rb1.velocity = posLazerCenter.up * -1 *lazerSpeed;

        GameObject leftLAzer = Instantiate(lazer, posLazerLeft.position, Quaternion.Euler(0, 0, -90));
        Rigidbody2D rb2 = leftLAzer.GetComponent<Rigidbody2D>();
        rb2.velocity = posLazerLeft.up * -1 * lazerSpeed;

        GameObject rightLAzer = Instantiate(lazer, posLazerRight.position, Quaternion.Euler(0, 0, -90));
        Rigidbody2D rb3 = rightLAzer.GetComponent<Rigidbody2D>();
        rb3.velocity = posLazerRight.up * -1 * lazerSpeed;

        yield return null; // Trả về ngay lập tức
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
