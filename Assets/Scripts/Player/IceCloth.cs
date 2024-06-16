using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceCloth : MonoBehaviour
{
    
    private Frezze frezze;
    public bool cloth = true;
    private void Awake()
    {
        frezze = GetComponent<Frezze>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloth") ){
            Destroy(collision.gameObject); // lấy được item, xóa item đó khỏi map
            frezze.Heal(100);
            cloth = false;
        }
    }
}
