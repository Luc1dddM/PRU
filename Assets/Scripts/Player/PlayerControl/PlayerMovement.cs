using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private Animator animator;
    public float movementSpeed = 5f;
    public bool facingRight = true;
    public float jumpPower = 500f;
    public PlayerGroundCollider playerGroundCollider;


    // Start is called before the first frame update
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("movement", Mathf.Abs(playerBody.velocity.x));
        animator.SetBool("isGrounded", playerGroundCollider.isGrounded);
    }

    public void Move(float horizontalInput)
    {
        if (horizontalInput != 0f)
        {
            CheckFacingDirection(horizontalInput);
            float horizontalVelocity = horizontalInput * movementSpeed;
            playerBody.velocity = new Vector2(horizontalVelocity, playerBody.velocity.y);
        }
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

    public void Jump()
    {
        if (playerGroundCollider.isGrounded)
        {
            playerBody.AddForce(new Vector2(0f, jumpPower));
        }
    }
}
