using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wingshoes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<doublejump>().EnableDoubleJump();
            Destroy(gameObject); // Destroy the amulet after collection
        }
    }
}
