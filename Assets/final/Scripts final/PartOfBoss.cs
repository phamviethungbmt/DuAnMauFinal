using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PartOfBoss : MonoBehaviour
{
    public int heathPart = 10;
    public int heathPartCurrent;
    public Slider heathPartSilder;

    public GameObject bulletPrefab;
    public Transform firePoint; // điểm viên đạn bắn
    public float bulletSpeed = 20f;
    public int burstCount = 5; // số lượng đạn bắn ra
    public float burstDelay = 0.1f; // thời gian chờ cho mỗi đợt bắn
    public float spreadAngle = 45f; // góc

    public GameObject explosionEffect;
    public GameObject explosionBullet;

    public int scoreAdd = 10;
    

    private void Start()
    {
        heathPartCurrent = heathPart;
        heathPartSilder.maxValue = heathPart;
        heathPartSilder.value = heathPartCurrent;

        StartCoroutine(BurstFireRoutine());
        StartCoroutine(SpreadFireRoutine());
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        { 
            Vector3 posExpolsive = collision.transform.position;

            GameObject a = Instantiate(explosionBullet, posExpolsive, Quaternion.identity);
            //Xóa vụ nổ
            Destroy(a, 0.9f);
            Destroy(collision.gameObject);
            heathPartCurrent -= 1;
            updateSliderPart();
        }

        if(heathPartCurrent <=0)
        {
            Destroy(gameObject);
            GameObject a = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            //Xóa vụ nổ
            Destroy(a, 0.9f);
            Destroy(heathPartSilder.gameObject);

            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeScore(scoreAdd);
            }
        }
        
    }
    

    public void updateSliderPart()
    {
        heathPartSilder.value = heathPartCurrent;
    }
    IEnumerator BurstFireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(BurstFire());
        }
    }

    IEnumerator BurstFire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            ShootBullet(Vector3.forward);
            yield return new WaitForSeconds(burstDelay);
        }
    }

    IEnumerator SpreadFireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            SpreadFire();
        }
    }

    void SpreadFire()
    {
        float angleStep = spreadAngle / (burstCount - 1);
        float angle = -spreadAngle / 2;

        for (int i = 0; i < burstCount; i++)
        {
            float bulletDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float bulletDirZ = firePoint.position.z + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, firePoint.position.y, bulletDirZ);
            Vector3 bulletDir = (bulletMoveVector - firePoint.position).normalized;

            ShootBullet(bulletDir);

            angle += angleStep;
        }
    }

    void ShootBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
        }
        else
        {
            //Debug.Log("Rigidbody không được gắn vào Bullet_Saw_Boss(Clone).");
        }
    }
}
