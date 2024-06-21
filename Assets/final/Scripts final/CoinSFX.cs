using Unity.VisualScripting;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioClip coinSound; // Biến để lưu trữ âm thanh nhặt đồng xu
    public AudioSource coinSource;
    private void Start()
    {
        coinSource = gameObject.AddComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra nếu đối tượng va chạm là Player
        {
            //coinSource.PlayOneShot(coinSound);
            AudioSource.PlayClipAtPoint(coinSound, transform.position); // Phát âm thanh tại vị trí của đồng xu
            Destroy(gameObject,0.5f); // Hủy đối tượng đồng xu sau khi nhặt
        }
    }
}