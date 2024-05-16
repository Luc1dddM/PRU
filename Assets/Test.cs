using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // Speed of the player
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveDirection = 0f;

        if (Input.GetKey(KeyCode.A))  // Check if 'A' key is pressed
        {
            moveDirection = -1f;  // Move left
        }
        else if (Input.GetKey(KeyCode.D))  // Check if 'D' key is pressed
        {
            moveDirection = 1f;  // Move right
        }

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);  // Apply movement
    }
}
