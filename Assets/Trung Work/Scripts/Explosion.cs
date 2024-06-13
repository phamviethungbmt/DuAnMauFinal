using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioSource audioExplosion;
    GameObject playerMain;
    void Start()
    {
         playerMain= GameObject.FindGameObjectWithTag("Player");
        audioExplosion.Play();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rbEplosion = playerMain.GetComponent<Rigidbody2D>();
            Vector2 directionExplosion = new Vector2(0, 5);
            rbEplosion.AddForce(directionExplosion, ForceMode2D.Impulse);
        }
    }
}
