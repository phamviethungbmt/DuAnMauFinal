using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public Transform bulletPos;
    [SerializeField] private GameObject bulletPrefab;

    private AudioSource AudioSource;
    public AudioClip bulletClip;



    //Controller controller;
    void Start()
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
       // controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire()
    {
        AudioSource.PlayOneShot(bulletClip);
        GameObject bulletPrf = Instantiate(bulletPrefab, bulletPos.position,transform.rotation);
        Destroy(bulletPrf,5);
        //Debug.Log(controller.IsFacingRight);
       
    }
}
