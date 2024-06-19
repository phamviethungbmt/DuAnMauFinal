using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform posA, posB;
    Vector2 posTarget;
    [SerializeField] private float speedDrone;
    private SpriteRenderer spriteDrone;
    private Animator animDrone;
    private Transform posPlayer;
    float speedDroneShoot;
    [SerializeField] private GameObject bulletDrone;
    [SerializeField] private Transform bulletDronePoint;
    float dirXBullet;
    [SerializeField] private AudioSource soundDeath;
    [SerializeField] private AudioSource soundShootByDrone;
    private Rigidbody2D rb;
    private Collider2D coll;

    // Khoảng cách tối đa để phát hiện người chơi trên trục x
    [SerializeField] private float playerDetectionRangeX = 7f;

    //hung
    public int takeScore = 5;

    void Start()
    {
        posTarget = posB.position;
        animDrone = GetComponent<Animator>();
        spriteDrone = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        posPlayer = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
    }

    void Update()
    {
        // Di chuyển giữa posA và posB
        if (Vector2.Distance(transform.position, posA.position) < .1f)
        {
            posTarget = posB.position;
            spriteDrone.flipX = true;
        }
        if (Vector2.Distance(transform.position, posB.position) < .1f)
        {
            posTarget = posA.position;
            spriteDrone.flipX = false;
        }
        transform.position = Vector2.MoveTowards(transform.position, posTarget, speedDrone * Time.deltaTime);

        if (posPlayer != null)
        {
            // Kiểm tra sự xuất hiện của người chơi theo trục x
            if (Mathf.Abs(posPlayer.position.x - transform.position.x) < playerDetectionRangeX &&
                Mathf.Abs(posPlayer.position.y - transform.position.y) < 1f)
            {
                // Hướng quay của Enemy theo người chơi
                if (transform.position.x > posPlayer.position.x)
                {
                    spriteDrone.flipX = false;
                }
                else
                {
                    spriteDrone.flipX = true;
                }

                // Dừng di chuyển và bắn
                speedDrone = 0;
                Shoot();
            }
            else
            {
                // Nếu người chơi không trong phạm vi, tiếp tục di chuyển
                speedDrone = 5;
            }
        }
        else
        {
            // Nếu tìm được tim > 0 thì reset màn chơi
            // Nếu tim = 0 thì hiện panel reset hoặc quay lại
        }
    }

    void Shoot()
    {
        speedDroneShoot += Time.deltaTime;
        if (speedDroneShoot > 1)
        {
            GameObject t = Instantiate(bulletDrone, bulletDronePoint.position, Quaternion.identity);
            if (spriteDrone.flipX == false)
            {
                t.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                t.GetComponent<SpriteRenderer>().flipX = true;
            }
            speedDroneShoot = 0;
            soundShootByDrone.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool destroyEnemy = false;
        // Xử lý va chạm với đạn
        if (collision.gameObject.CompareTag("Bullet"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;
            animDrone.SetTrigger("Death");
            soundDeath.Play();
            speedDroneShoot = 0;
            Destroy(collision.gameObject);
            destroyEnemy = true;
            Destroy(gameObject, 0.6f);
        }

        //hung
        //Bị tiêu diệt thì + điểm
        if (posPlayer != null)
        {
            PlayerHealth playerHealth = posPlayer.GetComponent<PlayerHealth>();
            if (destroyEnemy)
            {
                Debug.Log("quái bị tiêu diệt");

                if (playerHealth != null)
                {
                    playerHealth.TakeScore(takeScore);
                }
            }
        }
    }
}
