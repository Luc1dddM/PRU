using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMove : MonoBehaviour
{
    private bool isOnGroundIce = false; // Biến kiểm tra trạng thái
    private bool stopSliding = false;
    private float slideSpeed = 2f;
    private float defaultSlideSpeed;
    private bool isJumpingLeft = false;
    private bool isJumpingRight = false;
    private bool isJumping = false;

    private void Awake()
    {
        defaultSlideSpeed = slideSpeed;
    }

    private void Update()
    {
        // Kiểm tra phím nhảy và hướng nhảy
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isJumpingLeft = true;
            isJumpingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isJumpingLeft = false;
            isJumpingRight = true;
        }

        // Kiểm tra phím space để nhảy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }

        // Dừng trượt khi nhấn phím ngược lại
        if (isOnGroundIce)
        {
            if (isJumpingLeft && Input.GetKeyDown(KeyCode.RightArrow))
            {
                stopSliding = true;
            }
            else if (isJumpingRight && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                stopSliding = true;
            }

            if (stopSliding && !Input.anyKey)
            {
                stopSliding = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isOnGroundIce && !stopSliding && !isJumping)
        {
            Vector2 pos = transform.position;
            float s = slideSpeed * Time.fixedDeltaTime;

            if (isJumpingLeft)
            {
                pos.x -= s; // Trượt sang trái
            }
            else if (isJumpingRight)
            {
                pos.x += s; // Trượt sang phải
            }

            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ice"))
        {
            isOnGroundIce = true; // Đang đứng trên Ice
            if (isJumping)
            {
                isJumping = false; // Đã rớt xuống, ngừng nhảy
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ice"))
        {
            isOnGroundIce = false; // Không còn đứng trên Ice
            stopSliding = false;
        }
    }
}
