using System.Collections;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class crabMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform limitLeft,limitRight;
    [SerializeField] private float speed;
    private SpriteRenderer crabSprite;
    Animator animCrab;
    public GameObject explosionEffect;
    private Rigidbody2D rb;
    private float direction;
    private float healthCrab = 100;
    private Renderer renderer;
    private bool isVisible;
    private void Start()
    {
        crabSprite = GetComponent<SpriteRenderer>();
        animCrab = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        direction = 0;
    }
    void Update()
    {
        // AI di chuyển theo Player
        if (transform.position.x < player.position.x || transform.position.x < limitLeft.position.x)
        {
            crabSprite.flipX = true;
            direction = 1;
        }
        if (transform.position.x > player.position.x || transform.position.x > limitRight.position.x)
        {
            crabSprite.flipX = false;
            direction = -1;
        }
        rb.velocity = new Vector2(direction * speed, 0);
        //Nếu người chơi di chuyển mà AI nằm ngoài tầm nhìn Camera thì sẽ tự động biến mất
        if (!renderer.isVisible && isVisible )
        {
            Destroy(gameObject);
        }
        isVisible=renderer.isVisible;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu nó va chạm với Player thì sẽ tự động dừng lại, sau đó sẽ phát nổ
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Explosion());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // Nếu con cua va chạm với đạn của nhân vật thì sẽ bị trừ máu, hết máu thì chết, xóa đạn
            healthCrab -= 30;
            if (healthCrab <= 0)
            {
                animCrab.SetTrigger("Death2");
                Destroy(gameObject, 0.6f);
                Destroy(collision.gameObject, 0.6f);
            }
            else
            {
                animCrab.SetTrigger("Hurt");
                Destroy(collision.gameObject, 0.6f);
            }
        }
    }
    // Hiệu ứng nổ của quái
    IEnumerator Explosion()
    {
        yield return null;
        Destroy(gameObject);
        GameObject a=Instantiate(explosionEffect,transform.position,Quaternion.identity);
        Destroy(a,0.9f);
    }
}
