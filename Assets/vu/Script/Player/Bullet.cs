using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
  
    [SerializeField] private float bulletForce;
    Rigidbody2D rb;
    Controller controller;
    public Vector2 DirectionShoot;
    //{
    //    set
    //    {

    //        if (controller.IsFacingRight)
    //        {
    //            DirectionShoot = transform.right * bulletForce;
    //        }
           
    //    }
    //}
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = FindAnyObjectByType<Controller>();
        if (controller.IsFacingRight)
        {
            DirectionShoot = new Vector2(1, 0);
        }
        else
        {
            DirectionShoot = new Vector2(-1, 0);
        }

        rb.AddForce(DirectionShoot*bulletForce,ForceMode2D.Impulse);
       

        //  rb.velocity=new Vector2 (transform.position.x*bulletForce,rb.velocity.y);
    }

    // Update is called once per frame
 
   
}
