using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class falltrap : MonoBehaviour
{
    public GameObject Object;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            rb.isKinematic = false;
            StartCoroutine(cd());
        }
    }
    IEnumerator cd()
    {
        float tg = 10f;
        while(tg>0)
        {
            Object.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Object.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            tg--;
        }
        Destroy(this.gameObject);
    }
}
