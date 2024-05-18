using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float walkSpeed = 8f;
    public float jumpSpeed = 0.0f;

    private float moveInput;
    public bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;

    public PhysicsMaterial2D bounceMat, normalMat;
    public bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(jumpSpeed == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        }


        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.2f),
            new Vector2(1.12f, 0.30f), 0f, groundMask);

        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            jumpSpeed += 0.6f;

        }

        if(jumpSpeed > 0 && !isGrounded)
        {
            rb.sharedMaterial = bounceMat;
        }
        else
        {
            rb.sharedMaterial = normalMat;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f,rb.velocity.y);
        }

        if (jumpSpeed >= 30f && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpSpeed;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("resetJump", 0.3f);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed, jumpSpeed);
                jumpSpeed = 0f;
            }
            canJump = true;
        }


    }

    void resetJump()
    {
        
        jumpSpeed = 0.0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1.2f), new Vector2(1.12f, 0.20f));
    }

    /*    // OnCollisionEnter2D is called when this object collides with another object
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Check if the player collides with a ground object (e.g., platform, floor)
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true; // Player is grounded
            }
        }*/
}
