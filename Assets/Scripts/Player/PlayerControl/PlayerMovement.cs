using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 8f;
    public float jumpSpeed = 0.0f;

    private float moveInput;
    public bool isGrounded;
    private Rigidbody2D rb;
    public LayerMask groundMask;
    private Animator animator;
    public bool facingRight = true;

    public bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (jumpSpeed == 0.0f && isGrounded)
        {
            CheckFacingDirection(moveInput);
            animator.SetFloat("Movement", Mathf.Abs(rb.velocity.x));
            rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        }

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.72f),
            new Vector2(1.12f, 0.30f), 0f, groundMask);

        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            jumpSpeed += Time.deltaTime * 70f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }

        if (jumpSpeed >= 30f && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpSpeed;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("resetJump", 0.1f);
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
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.72f), new Vector2(1.12f, 0.20f));
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
        // Store the current velocity before transitioning
        SceneController.Instance.playerVelocity = rb.velocity;
        float height = 2f * Camera.main.orthographicSize;
        Vector3 currentPosition = rb.position;
        if (collision.CompareTag("Enter"))
        {
            Vector3 sceneSize = new Vector3(0, height - 15f, 0);
            SceneController.Instance.LoadNextScene(currentPosition, sceneSize);
        }
        else if (collision.CompareTag("Exit"))
        {
            // Store the current velocity before transitioning
            Vector3 sceneSize = new Vector3(0, height - 17f, 0);
            SceneController.Instance.LoadBackScene(currentPosition, sceneSize);
        }
    }
}
