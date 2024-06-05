using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;
    Animator animCrab;
    public GameObject explosionEffect;
    private float healthCrab = 4;
    GameObject playerMain; //Tham chiếu đến đối tượng Player
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        seeker= GetComponent<Seeker>();
        animCrab = GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        playerMain = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }
    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    private void PathFollow()
    {
        if (path==null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force=direction*speed*Time.deltaTime;
        if(jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale=new Vector3(-1f*Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            }
            else if (rb.velocity.x < -0.5f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position,target.transform.position) <activateDistance;
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Nếu nó va chạm với Player thì sẽ tự động phát nổ
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
        GameObject a = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        //Thêm lực đẩy cho viên đạn để khiến Player bị nhảy lên một đoạn
        Rigidbody2D rbBullet = playerMain.GetComponent<Rigidbody2D>();
        Vector2 directionExplosion = new Vector2(0, 7);
        rbBullet.AddForce(directionExplosion, ForceMode2D.Impulse);
        //Xóa vụ nổ
        Destroy(a, 0.9f);
    }
}
