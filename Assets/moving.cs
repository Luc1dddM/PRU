using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to set the movement speed
    public float jumpForce = 10f; // Adjust this value to set the jump force
    private bool isGrounded; // Flag to check if the player is grounded

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal input (left or right arrow keys, or A/D keys)
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f; // Move right
        }

        // Calculate the movement amount based on the input and speed
        float movement = horizontalInput * moveSpeed * Time.deltaTime;

        // Move the player horizontally
        transform.Translate(new Vector3(movement, 0f, 0f));

        // Check if the Space key is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Apply jump force to the player rigidbody
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; // Player is no longer grounded after jumping
        }
    }

    // OnCollisionEnter2D is called when this object collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with a ground object (e.g., platform, floor)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Player is grounded
        }
    }
}
