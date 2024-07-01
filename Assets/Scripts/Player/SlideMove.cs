using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMove : MonoBehaviour
{
    public float slideSpeed = 10f;
    public float friction = 0.5f;
    public bool isSliding = false;

    private Rigidbody2D rb;
    private Animator animator;
    private bool onIce = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding) // Nếu đang trượt
        {
            if (Input.GetAxisRaw("Horizontal") != 0)  // Nếu có đầu vào từ phím
            {
                StopSliding();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isSliding)
        {
            Vector2 slideDirection = new Vector2(transform.localScale.x, 0).normalized; // Xác định hướng trượt dựa trên hướng
            rb.velocity = new Vector2(slideDirection.x * slideSpeed, rb.velocity.y); // Đặt vận tốc của Rigidbody để trượt
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ice")) // trên Ice
        {
            if (!isSliding && rb.velocity.y < 0) // Nhân vật đang rơi xuống
            {
                StartSliding();
            }
        }
    }

    private void StartSliding()
    {
        isSliding = true;
        animator.SetBool("IsSliding", true);
        rb.velocity = new Vector2(transform.localScale.x * slideSpeed, rb.velocity.y);
    }

    private void StopSliding()
    {
        isSliding = false;
        animator.SetBool("IsSliding", false);
        rb.velocity = new Vector2(rb.velocity.x * friction, rb.velocity.y); // Áp dụng ma sát để làm chậm trượt
    }
}
