using System.Collections;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class crabMovement : MonoBehaviour
{
    private  Transform player;
    [SerializeField] private float speed;
    private float jump;
    private SpriteRenderer crabSprite;
    Animator animCrab;
    public GameObject explosionEffect;
    private Rigidbody2D rb;
    private float direction;
    private float healthCrab = 4;
    //Khai báo Camera
    private Renderer renderer;
    private bool isVisible;
    GameObject playerMain; //Tham chiếu đến đối tượng Player
    private void Start()
    {
        crabSprite = GetComponent<SpriteRenderer>();
        animCrab = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        direction = 0;
        playerMain = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        // AI di chuyển theo Player
        if (transform.position.x < player.position.x)
        {
            crabSprite.flipX = true;
            direction = 1;
            jump = 0;
        }
        if (transform.position.x > player.position.x)
        {
            crabSprite.flipX = false;
            direction = -1;
            jump = 0;
        }
        rb.velocity = new Vector2(direction * speed, jump);
        //Nếu người chơi di chuyển mà AI nằm ngoài tầm nhìn Camera thì sẽ tự động biến mất
        //if (!renderer.isVisible && isVisible)
        //{
        //    Destroy(gameObject);
        //}
        //isVisible=renderer.isVisible;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu nó va chạm với Player thì sẽ tự động phát nổ
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Explosion());
            //Thêm lực đẩy cho viên đạn để khiến Player bị nhảy lên một đoạn
            Rigidbody2D rbBullet = playerMain.GetComponent<Rigidbody2D>();
            Vector2 directionExplosion = new Vector2(0, 7);
            rbBullet.AddForce(directionExplosion, ForceMode2D.Impulse);
        }
        //Nếu quái chạm với tường thì sẽ tự động phát nổ
        if (collision.gameObject.tag == "DeathZone")
        {
            StartCoroutine(Explosion());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // Nếu con cua va chạm với đạn của nhân vật thì sẽ bị trừ máu, hết máu thì chết, xóa đạn
            healthCrab -= 1;
            if (healthCrab == 1)
            {
                animCrab.SetTrigger("AlmostDead");
                Destroy(collision.gameObject);
            }
            else
            {
                animCrab.SetTrigger("Hurt");
                Destroy(collision.gameObject);
            }
            if (healthCrab <= 0)
            {
                animCrab.SetTrigger("Death2");
                Destroy(gameObject, 0.4f);
                Destroy(collision.gameObject);
            }
        }
    }
    // Hiệu ứng nổ của quái
    IEnumerator Explosion()
    {
        yield return null;
        Destroy(gameObject);
        GameObject a=Instantiate(explosionEffect,transform.position,Quaternion.identity);
        //Xóa vụ nổ
        Destroy(a,0.9f);
    }
}
