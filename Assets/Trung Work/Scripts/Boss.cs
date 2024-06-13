using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject lazePrefab,lazeCenterPrefab,missleUpPrefab, missleDownPrefab;
    [SerializeField] private Transform posBirthLazeLeft,posBirthLazeRight,posBirthLazeCenter,posMissleUp,posMissleDown;
    Rigidbody2D rbPlayer;
    private Transform posPlayer;
    int skill;
    private void Start()
    {
        skill = 1;
        posPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        while (true)
        {
            StartCoroutine(Shoot());
            yield return new WaitForSeconds(2f);
            StartCoroutine(ShootMissle());
            yield return new WaitForSeconds(11f);
        }
    }
    IEnumerator SwitchAttck()
    {
        yield return new WaitForSeconds(1);
        skill = Random.Range(1, 3);
    }
    IEnumerator Shoot()
    {
            yield return new WaitForSeconds(1f);
            GameObject a = Instantiate(lazePrefab, posBirthLazeLeft.position, posBirthLazeLeft.rotation);
            Destroy(a, 1.5f);
            GameObject b = Instantiate(lazePrefab, posBirthLazeRight.position, posBirthLazeRight.rotation);
            Destroy(b, 1.5f);
            GameObject c = Instantiate(lazeCenterPrefab, posBirthLazeCenter.position, posBirthLazeCenter.rotation);
            Destroy(c, 1.5f);
            yield return new WaitForSeconds(2f);
    }
    IEnumerator ShootMissle()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject b = Instantiate(missleUpPrefab, posMissleUp.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            Destroy(b, 3f);
            GameObject c = Instantiate(missleDownPrefab, new Vector2(posPlayer.position.x, posMissleDown.position.y), Quaternion.identity);
        }
    }
}
