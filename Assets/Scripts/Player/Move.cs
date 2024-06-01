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


    private float moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    


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

        }

        if (jumpSpeed >= 27f && isGrounded)
        {
            float tempx = moveInput * walkSpeed * jumpDistance;
            float tempy = jumpSpeed;
            
            animator.SetBool("IsRecharge", false);
            rb.velocity = new Vector2(tempx, tempy);
            canJump = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * walkSpeed * jumpDistance, jumpSpeed);
                
                animator.SetBool("IsRecharge", false);

            }
            canJump = true;
        }


    }



    private void LateUpdate()
    {
        if (!isGrounded)
        {
            jumpSpeed =  0f;
            
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


}
