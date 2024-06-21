using Unity.VisualScripting;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [SerializeField] private GameObject soundCoinObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra nếu đối tượng va chạm là Player
        {
            //coinSource.PlayOneShot(coinSound);
            //AudioSource.PlayClipAtPoint(coinSound, transform.position); // Phát âm thanh tại vị trí của đồng xu
            GameObject soundCoin = Instantiate(soundCoinObj, transform.position, Quaternion.identity);
            Destroy(soundCoin,1.5f);
            Destroy(gameObject,0.5f); // Hủy đối tượng đồng xu sau khi nhặt
        }
    }
}