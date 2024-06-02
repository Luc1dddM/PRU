using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float walkSpeed = 8f;
    public float jumpSpeed = 0.0f;
    public float jumpDistance = 2f;
    public bool isGrounded;
    public LayerMask groundMask;
    public bool facingRight = true;
    public bool canJump = true;
   
    public Collider2D bodycollider;
    public Collider2D Footcollider;
    public PhysicsMaterial2D normalMa;
    public PhysicsMaterial2D bounceMa;



    public float jumpDistance = 2f;
    public bool isGrounded;
    public LayerMask groundMask;
    public bool facingRight = true;
    public bool canJump = true;
    public Collider2D bodycollider;
    public Collider2D Footcollider;
    public PhysicsMaterial2D normalMa;
    public PhysicsMaterial2D bounceMa;



    private float moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    public bool canDoubleJump; // kiểm tra trạng thái nhảy



    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Footcollider.sharedMaterial = normalMa;
        rb.gravityScale = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetBool("IsJumping", !isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
        if (jumpSpeed == 0.0f && isGrounded)
        {
            
           CheckFacingDirection(moveInput);
            animator.SetFloat("Movement", Mathf.Abs(rb.velocity.x));
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        }

        if (isGrounded)
        {
            bodycollider.sharedMaterial = normalMa;
            canDoubleJump = false; // Đặt lại trạng thái nhảy khi tiếp đất
        }
        else
        {
            bodycollider.sharedMaterial = bounceMa;
        }





        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.64f)
            , new Vector2(1.1f, 0.05f), 0f, groundMask);
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            jumpSpeed += Time.deltaTime * 60f;
            animator.SetBool("IsRecharge", true);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f,rb.velocity.y);
            animator.SetFloat("Movement", Mathf.Abs(rb.velocity.x));
            canDoubleJump = true; // Đặt lại trạng thái nhảy khi đang ở trên mặt đất
        }

        if (jumpSpeed >= 27f && isGrounded)
        {
            float tempx = moveInput * walkSpeed * jumpDistance;
            float tempy = jumpSpeed;
            
            animator.SetBool("IsRecharge", false);
            rb.velocity = new Vector2(tempx, tempy);
            canJump = false;
            canDoubleJump = true; // Đặt trạng thái nhảy khi nhảy lên

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed * jumpDistance, jumpSpeed              
                animator.SetBool("IsRecharge", false);
                canDoubleJump = true; // Đặt lại trạng thái nhảy khi đang ở trên mặt đất
            }
            canJump = true;
        }
    }




    private void LateUpdate()
    {
        if (!isGrounded)
        {
            jumpSpeed = 0f;
            canDoubleJump = true; // Đặt lại trạng thái nhảy khi đang ở trên mặt đất

        }
        else
    {
            canDoubleJump = false; // Đặt lại trạng thái nhảy khi đang ở trên mặt đất
    }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            canJump = true;
        }
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.64f), new Vector2(1.1f, 0.05f));
    }

    private void CheckFacingDirection(float horizontalInput)
    {
        if (facingRight && horizontalInput < 0f)
        {
            Flip();
        }
        else if (!facingRight && horizontalInput > 0f)
        {
            Flip();
        }
    }
    private void Flip()
    {
        var playerScale = transform.localScale;
        transform.localScale = new Vector3(playerScale.x * -1, playerScale.y, playerScale.z);
        facingRight = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enter"))
        {
            // Store the current velocity before transitioning
            SceneController.Instance.playerVelocity = rb.velocity;
            float height = 2f * Camera.main.orthographicSize;
            Vector3 currentPosition = rb.position;
            Vector3 sceneSize = new Vector3(0, height - 15f, 0);
            SceneController.Instance.LoadScene("SampleScene", currentPosition, sceneSize);
        }
        else if (collision.CompareTag("Exit"))
        {
            // Store the current velocity before transitioning
            SceneController.Instance.playerVelocity = rb.velocity;
            float height = 2f * Camera.main.orthographicSize;
            Vector3 currentPosition = rb.position;
            Vector3 sceneSize = new Vector3(0, height - 17f, 0);
            Debug.Log(sceneSize);
            SceneController.Instance.LoadScene("RuinsMap1", currentPosition, sceneSize);
        }
    }

    // OnCollisionEnter2D is called when this object collides with another object
/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with a ground object (e.g., platform, floor)
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpSpeed = 0.0f;
        }
    }*/
}