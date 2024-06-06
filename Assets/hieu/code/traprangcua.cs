using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traprangcua : MonoBehaviour
{
    public float speed = 5f;  // Tốc độ quay của bánh răng
    public float speeddc = 5f;  // Tốc độ di chuyển của bánh răng
    public Transform diema;  // Điểm A
    public Transform diemb;  // Điểm B
    private Vector3 diemmuctieu;  // Điểm mục tiêu hiện tại

    // Start is called before the first frame update
    void Start()
    {
        diemmuctieu = diema.position;  // Khởi tạo điểm mục tiêu là điểm A
    }

    // Update is called once per frame
    void Update()
    {
        // Di chuyển bánh răng đến điểm mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, diemmuctieu, speeddc * Time.deltaTime);

        // Kiểm tra nếu bánh răng đã đến gần điểm mục tiêu
        if (Vector3.Distance(transform.position, diemmuctieu) < 0.1f)
        {
            // Nếu điểm mục tiêu là điểm A, chuyển sang điểm B
            if (diemmuctieu == diema.position)
            {
                diemmuctieu = diemb.position;
            }
            // Nếu điểm mục tiêu là điểm B, chuyển sang điểm A
            else
            {
                diemmuctieu = diema.position;
            }
        }
    }

    private void FixedUpdate()
    {
        // Quay bánh răng
        transform.Rotate(0, 0, speed);
    }
}
