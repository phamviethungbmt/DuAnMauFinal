using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCop : MonoBehaviour
{
    //[SerializeField] private float speedBulletEnemy;
    //Rigidbody2D rb;
    //private SpriteRenderer sprite1;
    //float dirBullet=0;
    void Start()
    {
        //sprite1 = GameObject.FindAnyObjectByType<CopController>().GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //rb.velocity = dirBullet*transform.right * speedBulletEnemy;
        Destroy(gameObject, 5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        Destroy(gameObject,5);
    }
}
