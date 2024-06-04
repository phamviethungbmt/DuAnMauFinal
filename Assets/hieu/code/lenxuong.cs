using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class lenxuong : MonoBehaviour
{
    bool a;
    public float speed = 3f;
    public float n1;
    public float n2;  
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= n1)
        {
            a = true;
        }
        if (transform.position.y < n2)
        {
            a = false;
        }
        if (a)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (!a)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }
}
