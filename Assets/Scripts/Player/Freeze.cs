using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Freeze : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    public Sprite normalSprite;
    public Sprite skatingSprite;

    private SpriteRenderer spriteRenderer;
    private bool isFire = false;

    private Animator animator;
    private Move move;
    private bool cloth = true;
    private SlideMove slideMove ;
    
  

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        move = GetComponent<Move>();
        slideMove = GetComponent<SlideMove>();
        spriteRenderer.sprite = normalSprite;
    }

    void Start()
    {
        // Start the health adjustment coroutine
        StartCoroutine(AdjustHealth()); 
        StartCoroutine(TakeDame());
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount == 0)
        {
            spriteRenderer.sprite = skatingSprite;
            move.enabled = false;
            animator.enabled = false;
            slideMove.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Heal(8); // click chuột để phá băng, giảm 8
        }

        if (spriteRenderer.sprite == skatingSprite)
        {
            /*if (Input.GetMouseButtonDown(0)) 
            {
                Heal(8);
            }*/

            if (healthAmount == 100) {
                spriteRenderer.sprite = normalSprite; //Trở lại hình ảnh bình thường
                move.enabled = true;
                animator.enabled = true;
                slideMove .enabled = true;
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        healthAmount -= dmg; // Giảm máu khi gần fire
        if (healthAmount < 0) healthAmount = 0;
        healthBar.fillAmount = healthAmount / 100f; // Cập nhật thanh máu trên UI
    }

    public void Heal(float heal)
    {
        healthAmount += heal; // Tăng máu khi xa fire
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); // Giới hạn giá trị máu
        healthBar.fillAmount = healthAmount / 100f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            isFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            isFire = false;
        }
    }

    private IEnumerator AdjustHealth()
    {
        while (true)
        {
            if (healthAmount > 0 && isFire)
            {
                Heal(10);
            }
            yield return new WaitForSeconds(0.5f); // chờ 0.5s trước khi tăng
        }
    }
    private IEnumerator TakeDame()
    {    
        while (true)
        {
            if (healthAmount >= 0 && !isFire)
            {
                // sau khi lấy item
                if (cloth == false)
                {
                    yield return new WaitForSeconds(1f); // chờ 1s trước khi tăng
                    TakeDamage(8); // thanh đóng băng tăng 
                    
                }
                else if (cloth == true)
                {
                    yield return new WaitForSeconds(0.5f);
                    TakeDamage(10);
                  
                }
            }
            else
            {
                yield return null;
            }
            
        }
    }
   
    public void ActiveCloth()
    {
        Heal(100);
        cloth = false;
        Debug.Log("Hi");
        StopCoroutine(TakeDame());
        StartCoroutine(TakeDame());

    }

}
