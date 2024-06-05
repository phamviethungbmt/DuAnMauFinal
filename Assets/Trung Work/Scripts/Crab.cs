using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    public Transform player; // Vị trí của Player
    public float speed = 2f; // Tốc độ di chuyển của AI
    public float jumpForce = 5f; // Lực nhảy của AI
    public LayerMask groundLayer; // Lớp mặt đất để kiểm tra AI đang đứng trên mặt đất
    private Rigidbody2D rb;
    private bool isGrounded;
    public float Radius;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Di chuyển AI theo hướng của Player
        Vector2 direction = new Vector2(player.position.x - transform.position.x,0).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        // Kiểm tra xem AI có đang đứng trên mặt đất hay không
        isGrounded = Physics2D.OverlapCircle(transform.position, Radius, groundLayer);

        // Kiểm tra nếu Player nhảy lên cao hơn AI và AI đang đứng trên mặt đất, AI sẽ nhảy theo
        if (player.position.y > transform.position.y +2.5f && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Hiển thị vòng tròn để kiểm tra va chạm mặt đất trong Scene view (tùy chọn)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
