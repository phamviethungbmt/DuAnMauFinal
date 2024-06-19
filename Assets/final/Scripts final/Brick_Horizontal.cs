using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    bool a;
    public float speed = 3f;
    public float n1;
    public float n2;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= n1)
        {
            a = true;
        }
        if (transform.position.x < n2)
        {
            a = false;
        }
        if (a)
        {
            transform.Translate(-speed * Time.deltaTime,0 , 0);
        }
        if (!a)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
    private void Start()
    {
        PlatformEffector2D effector = gameObject.AddComponent<PlatformEffector2D>();
        effector.useOneWay = true;
        effector.useColliderMask = true;
    }
}
