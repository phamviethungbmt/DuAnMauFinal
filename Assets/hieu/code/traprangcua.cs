using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traprangcua : MonoBehaviour
{
    public float speed = 5f;
    public float speeddc = 5f;
    public Transform diema;
    public Transform diemb;
    private Vector3 diemmuctieu;
   
    // Start is called before the first frame update
    void Start()
    {
        diemmuctieu=diema.position;
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position = Vector3.MoveTowards(transform.position,diemmuctieu,speeddc*Time.deltaTime);
        if (Vector3.Distance(transform.position, diemmuctieu) < 0.1f)
        {
            if(transform.position==diema.position)
            {
                diemmuctieu=diemb.position;
            }            
            else
            {
                diemmuctieu = diema.position;
            }
        }      
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, speed);
    }
}
