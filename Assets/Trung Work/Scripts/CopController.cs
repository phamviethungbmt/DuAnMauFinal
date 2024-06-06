using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CopController : MonoBehaviour
{
    public Transform posLeft, posRight;
    public float speed;
    Vector2 posTarget;
    private SpriteRenderer spriteCop;
    public GameObject eyeRay;
    float direction = 0;
    public float distance;
    private Transform playerPos;
    private Animator anim;
    public GameObject bulletCop;
    public Transform posBullet,posBullet1,posBullet2;
    public float speedBulletAngle;
    float time;
    public AudioSource audioShootCop;
    void Start()
    {
        posTarget=posLeft.position;
        spriteCop=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform;
    }
    void Update()
    {
        if (Vector2.Distance(transform.position,posLeft.position) < .1f)
        {
            posTarget=posRight.position;
            spriteCop.flipX= true;
        }
        if (Vector2.Distance(transform.position, posRight.position) < .1f)
        {
            posTarget = posLeft.position;
            spriteCop.flipX = false;
        }
        if (spriteCop.flipX)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        transform.position=Vector2.MoveTowards(transform.position, posTarget, speed*Time.deltaTime);
        if (Mathf.Abs(transform.position.x-playerPos.position.x) < distance)
        {
            //Nếu quái ở bên phải người chơi
            if (transform.position.x > playerPos.position.x)
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
            speed = 3;
            anim.speed=1;
        }
    }
    void StartShoot()
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            GameObject t= Instantiate(bulletCop, posBullet.position, Quaternion.identity);
            GameObject t1 = Instantiate(bulletCop, posBullet1.position, posBullet1.rotation);
            Rigidbody2D r1=t1.GetComponent<Rigidbody2D>();
            Vector2 directionBullet1 = new Vector2(1, 1).normalized;
            r1.velocity = directionBullet1*speedBulletAngle;
            GameObject t2 = Instantiate(bulletCop, posBullet2.position, posBullet2.rotation);
            Rigidbody2D r2 = t1.GetComponent<Rigidbody2D>();
            Vector2 directionBullet2 = new Vector2(1, 1).normalized;
            r1.velocity = directionBullet1 * speedBulletAngle;
            if (spriteCop.flipX == false)
            {
                t.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                t.GetComponent<SpriteRenderer>().flipX = true;
            }
            audioShootCop.Play();
            time = 0;
        }
    }
}
