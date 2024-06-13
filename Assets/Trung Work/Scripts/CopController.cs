using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CopController : MonoBehaviour
{
    public Transform posLeft, posRight;
    [SerializeField] private float moveSpeedCop;
    private float speed;
    Vector2 posTarget;
    private SpriteRenderer spriteCop;
    float direction = 0;
    public float distance;
    private GameObject playerPos;
    private Animator anim;
    public GameObject bulletCop;
    public Transform posBullet,posBullet1,posBullet2;
    public float speedBulletAngle;
    float time=3;
    public AudioSource audioShootCop;
    float shootRate=5f;
    [SerializeField] private bool foundPlayer = false;
    [SerializeField] private GameObject EnemyIsDestroyedExplosion;
    void Start()
    {
        time = 0;
        speed = moveSpeedCop;
        posTarget =posLeft.position;
        spriteCop=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (!foundPlayer)
        {
            if (Vector2.Distance(transform.position, posLeft.position) < .1f)
            {
                posTarget = posRight.position;
                spriteCop.flipX = true;
            }
            if (Vector2.Distance(transform.position, posRight.position) < .1f)
            {
                posTarget = posLeft.position;
                spriteCop.flipX = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, posTarget, speed * Time.deltaTime);
        }
        if (spriteCop.flipX)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        if (foundPlayer && Vector2.Distance(playerPos.transform.position,transform.position) < 30)
        {
            //Nếu quái ở bên phải người chơi
            if (transform.position.x > playerPos.transform.position.x)
            {
                if (spriteCop.flipX)
                {
                    spriteCop.flipX = false;
                }
                if (spriteCop.flipX == false)
                {
                    speed = 0;
                    anim.SetBool("OnIdle", true);
                    anim.speed = 0;
                    StartShoot();
                }
            }
            //Nếu quái ở bên trái người chơi
            else
            {
                if (spriteCop.flipX==false)
                {
                    spriteCop.flipX = true;
                }
                if (spriteCop.flipX)
                {
                    anim.speed = 0;
                    speed = 0;
                    anim.SetBool("OnIdle", true);
                    StartShoot();
                }
            }
        }
        else
        {
            anim.SetBool("OnIdle", false);
            speed = moveSpeedCop;
            anim.speed=1;
        }
    }
    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, playerPos.transform.position-transform.position);
        if (ray.collider != null)
        {
            //Quét được gameObject có tên là Player thì sẽ tấn công người chơi
            if (ray.transform.name == "Player")
            {
                foundPlayer = true;
                Debug.DrawRay(transform.position, playerPos.transform.position - transform.position, Color.green);
            }
            else
            {
                foundPlayer = false;
                Debug.DrawRay(transform.position, playerPos.transform.position - transform.position, Color.red);
            }
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameObject enemyExplosion=Instantiate(EnemyIsDestroyedExplosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Destroy(enemyExplosion, 0.6f);
        }
    }
    void StartShoot()
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            GameObject t = Instantiate(bulletCop, posBullet.position, Quaternion.identity);
            Rigidbody2D r = t.GetComponent<Rigidbody2D>();
            r.velocity = posBullet.right*speedBulletAngle * direction;
            Destroy(t, 5f);
            GameObject t1 = Instantiate(bulletCop, posBullet1.position, posBullet1.rotation);
            Rigidbody2D r1 = t1.GetComponent<Rigidbody2D>();
            //Vector2 directionBullet1 = new Vector2(1,1).normalized;
            r1.velocity = posBullet1.right * speedBulletAngle * direction;
            Destroy(t1, 5f);
            GameObject t2 = Instantiate(bulletCop, posBullet2.position, posBullet2.rotation);
            Rigidbody2D r2 = t2.GetComponent<Rigidbody2D>();
            //Vector2 directionBullet2 = new Vector2(1,1).normalized;
            r2.velocity = posBullet2.right * speedBulletAngle * direction;
            Destroy(t2, 5f);
            time = 0;
            audioShootCop.Play();
        }
    }
}
