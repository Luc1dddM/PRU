using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float jumpSpeed = 0.0f;
    public bool isGrounded;
    public LayerMask groundMask;
    public bool facingRight = true;



    public Collider2D bodycollider;
    public Collider2D Footcollider;
    public PhysicsMaterial2D normalMa;
    public PhysicsMaterial2D bounceMa;

    private bool coinCollected = false;
    private bool canJump = true;
    private float jumpDistance = 2f;
    private float walkSpeed = 8f;
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
            , new Vector2(1.163f, 0.03f), 0f, groundMask);
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            CheckFacingDirection(moveInput);
            jumpSpeed += Time.deltaTime * 60f;
            animator.SetBool("IsRecharge", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            animator.SetFloat("Movement", Mathf.Abs(rb.velocity.x));
            canDoubleJump = true; // Đặt lại trạng thái nhảy khi đang ở trên mặt đất
        }

        if (jumpSpeed >= 30f && isGrounded)
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
                rb.velocity = new Vector2(moveInput * walkSpeed * jumpDistance, jumpSpeed);
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

    //Collect coin 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!coinCollected && collision.CompareTag("Coin"))
        {
            coinCollected = true;
            Destroy(collision.gameObject);
            CoinController.instance.coinCout++;
            StartCoroutine(ResetCoinCollected());

        }
    }

    private IEnumerator ResetCoinCollected()
    {
        yield return new WaitForEndOfFrame();
        coinCollected = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && CoinController.instance.coinCout == 3 && collision.CompareTag("Gate"))
        {
            CoinController.instance.coinEnough = true;
        }

    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.64f), new Vector2(1.163f, 0.03f));

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