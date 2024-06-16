using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAmulet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ShieldBehavior>().EnableShieldActivation();
            Destroy(gameObject); // Destroy the amulet after collection
        }
    }
}
