using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public Transform[] patrolPoints; // Các điểm tuần tra
    public float speed = 2f; // Tốc độ di chuyển của AI
    public float chaseSpeed = 4f; // Tốc độ khi đuổi theo
    public float detectionRange = 5f; // Phạm vi phát hiện người chơi
    public LayerMask playerLayer; // Lớp của người chơi
    public float jumpForce = 10f; // Lực nhảy của AI

    private int currentPointIndex = 0;
    private bool chasingPlayer = false;
    private Transform playerTransform;
    private Rigidbody2D rb;
    private bool facingRight = true;
    public LayerMask groundLayer;
    private Collider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (chasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        DetectPlayer();
    }

    void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            if (currentPointIndex == 0)
            {
                currentPointIndex = 1;
            }    
            if (currentPointIndex == 1)
            {
                currentPointIndex = 0;
            }
        }

        Flip(targetPoint.position.x);
    }

    void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, playerLayer);
        if (playerCollider != null)
        {
            chasingPlayer = true;
            playerTransform = playerCollider.transform;
        }
    }

    void ChasePlayer()
    {
        Vector2 targetPosition = new Vector2(playerTransform.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, chaseSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);

        // Kiểm tra nếu cần nhảy
        if (Mathf.Abs(playerTransform.position.y - transform.position.y) > 1f && IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        Flip(playerTransform.position.x);
    }

    void Flip(float targetX)
    {
        if ((facingRight && targetX < transform.position.x) || (!facingRight && targetX > transform.position.x))
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    bool IsGrounded()
    {
        bool ground= false;
        if (coll.IsTouchingLayers(groundLayer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
        return ground;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
