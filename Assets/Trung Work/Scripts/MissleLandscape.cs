using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLandscape : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosionMissle;
    private Rigidbody2D rb;
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity=transform.up*speed*Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Player")
        {
            GameObject t=Instantiate(explosionMissle,transform.position,Quaternion.identity);
            Destroy(gameObject);
            Destroy(t, 0.7f);
        }
    }
}
