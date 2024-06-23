﻿using System.Collections;
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
    private IceCloth cloth;
    private bool isFreezeSoundPlayed = false;
    AudioManager audioManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        move = GetComponent<Move>();
        cloth = GetComponent<IceCloth>();
        spriteRenderer.sprite = normalSprite;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        if (healthAmount == 0 && !isFreezeSoundPlayed)
        {
            spriteRenderer.sprite = skatingSprite;
            move.enabled = false;
            animator.enabled = false;
            audioManager.PlaySFX(audioManager.freeze);
            isFreezeSoundPlayed = true; // Đánh dấu là âm thanh freeze đã được phát
        }
        if (spriteRenderer.sprite == skatingSprite) {
            if (Input.GetMouseButtonDown(0))
            {
                audioManager.PlaySFX(audioManager.mealtingclick);
                Heal(8); // click chuột để phá băng, giảm 8
            }
        }


        if (spriteRenderer.sprite == skatingSprite && healthAmount == 100)
        {
                spriteRenderer.sprite = normalSprite; //Trở lại hình ảnh bình thường
                move.enabled = true;
                animator.enabled = true;
                audioManager.PlaySFX(audioManager.crackingIce);
                isFreezeSoundPlayed = false; // Đặt lại trạng thái của biến isFreezeSoundPlayed

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
        int i = 0;
        while (true)
        {
            if (healthAmount >= 0 && !isFire)
            {
                // sau khi lấy item
                if (cloth.cloth == false)
                {
                    if (i == 0)
                    {
                        yield return new WaitForSeconds(1f);
                    }

                    i++;
                    TakeDamage(8); // thanh đóng băng tăng 
                    yield return new WaitForSeconds(1f); // chờ 1s trước khi tăng
                }
                else
                {
                    TakeDamage(10);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                yield return null;
            }

        }
    }
}
