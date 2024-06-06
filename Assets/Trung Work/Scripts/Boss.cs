using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject lazePrefab,lazeCenterPrefab;
    [SerializeField] private Transform posBirthLazeLeft,posBirthLazeRight,posBirthLazeCenter;
    Rigidbody2D rbPlayer;
    private void Start()
    {
        StartCoroutine(Shoot());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject a = Instantiate(lazePrefab, posBirthLazeLeft.position, posBirthLazeLeft.rotation);
            Destroy(a, 2);
            GameObject b = Instantiate(lazePrefab, posBirthLazeRight.position, posBirthLazeRight.rotation);
            Destroy(b, 2);
            yield return new WaitForSeconds(3);
            GameObject c = Instantiate(lazeCenterPrefab, posBirthLazeCenter.position, posBirthLazeCenter.rotation);
            Destroy(c, 2);
            yield return new WaitForSeconds(3);
        }
    }
}
