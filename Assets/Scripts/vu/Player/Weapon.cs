using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public Transform bulletPos;
    [SerializeField] private GameObject bulletPrefab;
    //Controller controller;
    void Start()
    {
       // controller = GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fire()
    {
        GameObject bulletPrf = Instantiate(bulletPrefab, bulletPos.position,transform.rotation);
        //Debug.Log(controller.IsFacingRight);
       
    }
}
