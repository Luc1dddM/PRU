using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class doublejump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Move moveScript;
    private ParticleSystem doubleJumpParticle; // Thêm biến ParticleSystem

    // Thêm biến cho vận tốc nhảy boost
    public float boostJumpSpeed = 15f; // Đặt giá trị mặc định cho vận tốc nhảy boost
    private bool canDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        moveScript = gameObject.GetComponent<Move>();
        doubleJumpParticle = gameObject.GetComponentInChildren<ParticleSystem>(); // Lấy component ParticleSystem
        canDoubleJump = false; // Ban đầu không cho phép nhảy đôi

        // Điều chỉnh số lượng hạt phát ra
        var emission = doubleJumpParticle.emission;
        emission.rateOverTime = 50f; // Đặt số lượng hạt phát ra mỗi giây
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra điều kiện để thực hiện nhảy đôi
        if (Input.GetKeyDown(KeyCode.Space) && !moveScript.isGrounded && canDoubleJump)
        {
            // Thực hiện nhảy boost với vận tốc boostJumpSpeed
            rb.velocity = new Vector2(rb.velocity.x, boostJumpSpeed);

            // Kích hoạt Particle System cho double jump
            doubleJumpParticle.Play();

            canDoubleJump = false; // Sau khi nhảy đôi, không cho phép nhảy đôi lần nữa
        }

        // Reset lại khi nhân vật chạm đất
        if (moveScript.isGrounded)
        {
            canDoubleJump = true; // Cho phép nhảy đôi khi chạm đất

            // Dừng Particle System ngay lập tức khi chạm đất
            StopAndClearParticleSystem(doubleJumpParticle);
        }
    }

    private void StopAndClearParticleSystem(ParticleSystem ps)
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
