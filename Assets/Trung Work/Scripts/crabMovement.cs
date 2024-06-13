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
    [SerializeField] private Transform posLeft,posRight,limitCanSeeLeft,limitCanSeeRight;
    private Vector2 posCrabTarget;
    private Transform limitCanSee;
    private bool crabSawYou;
    [SerializeField] private GameObject ExplosionWhenCrabIsDefeated;
    private void Start()
    {
        limitCanSee = null;
        crabSawYou = false;
        crabSprite = GetComponent<SpriteRenderer>();
        animCrab = GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        direction = 0;
        playerMain = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        posCrabTarget=posLeft.position;
    }
    void Update()
    {
        if (!crabSawYou)
        {
            if (Vector2.Distance(transform.position, posLeft.position) < .1f)
            {
                crabSprite.flipX = true;
                posCrabTarget = posRight.position;
            }
            if (Vector2.Distance(transform.position, posRight.position) < .1f)
            {
                crabSprite.flipX = false;
                posCrabTarget = posLeft.position;
            }
            transform.position = Vector2.MoveTowards(transform.position, posCrabTarget, speed * Time.deltaTime);
        }
        else
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
            transform.position=Vector2.MoveTowards(transform.position,new Vector2(player.position.x,0), speed * Time.deltaTime);
        }
        //Nếu người chơi di chuyển mà AI nằm ngoài tầm nhìn Camera thì sẽ tự động biến mất
        //if (!renderer.isVisible && isVisible)
        //{
        //    Destroy(gameObject);
        //}
        //isVisible=renderer.isVisible;
    }
    private void FixedUpdate()
    {
        if (crabSprite.flipX)
        {
            limitCanSee = limitCanSeeRight;
        }
        else
        {
            limitCanSee = limitCanSeeLeft;
        }
        RaycastHit2D hit=Physics2D.Raycast(transform.position,limitCanSee.position - transform.position);
        if (hit.collider != null)
        {
            if (hit.transform.name == "Player")
            {
                Debug.DrawRay(transform.position, limitCanSee.position - transform.position, Color.green);
                crabSawYou = true;
            }
            else
            {
                Debug.DrawRay(transform.position, limitCanSee.position - transform.position, Color.red);
            }
        }
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
                GameObject explosion = Instantiate(ExplosionWhenCrabIsDefeated, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(explosion, 0.6f);
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
