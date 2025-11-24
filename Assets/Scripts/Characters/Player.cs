
using UnityEngine;
using System.Collections;

public class Player : Character
{

    [Header("Jump")]
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundRadius = 0.15f;

    private bool isGrounded;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 8f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 0.5f;

    private bool isDashing;
    private float dashTimer;
    private float nextDashTime;
    private float dashDirection;

    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint; // จุดเกิด (ลากจาก Inspector)
    [SerializeField] private float respawnDelay = 1f; // หน่วงก่อนเกิดใหม่
    private Vector3 defaultSpawnPos;
    private int startHealth = 100;

    public int Score { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            item.PickUp(this);
        }
    }

    void Start()
    {
        base.Intialize(100);
        defaultSpawnPos = transform.position;
    }

    
    protected override void Awake()
    {
        base.Awake();
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public override void Move(Vector2 input)
    {
        if (isDashing) return; 

        base.Move(input);      
    }

    public void TryDash(float direction)
    {
        if (Time.time < nextDashTime || isDashing) return;

        isDashing = true;
        dashTimer = dashDuration;
        dashDirection = Mathf.Sign(direction == 0 ? transform.localScale.x : direction);
        nextDashTime = Time.time + dashCooldown;
    }

    public void addPoint(int value)
    {
        Score += value;
        Debug.Log("Score: " + Score);
    }

    public void AddTemporarySpeed(int value, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(value, duration));
    }

    private IEnumerator SpeedBoostRoutine(int value, float duration)
    {
        moveSpeed += value;                       // เพิ่มความเร็วชั่วคราว
        yield return new WaitForSeconds(duration);
        moveSpeed -= value;                       // กลับสู่ค่าปกติ
    }

    public void takeDamage(int value)
    {
        Health -= value;
        if (Health <= 0)
        {
            Die();

        }
    }

    public void Heal(int value)
    {
        Health += value;
    }

    private System.Collections.IEnumerator RespawnRoutine()
    {
        // หยุดการเคลื่อนที่
        rb.linearVelocity = Vector2.zero;

        // หน่วงเวลาเล็กน้อย เผื่อเล่นอนิเมชั่นตาย
        yield return new WaitForSeconds(respawnDelay);

        // เลือกตำแหน่งเกิด: ถ้ามีตั้ง respawnPoint ไว้ก็ใช้ตัวนั้น ไม่งั้นใช้ตำแหน่งเริ่มเกม
        Vector3 targetPos = respawnPoint != null ? respawnPoint.position : defaultSpawnPos;
        transform.position = targetPos;

        // รีเซ็ตเลือด
        Health = startHealth;   // หรือ 100 ถ้าไม่ใช้ตัวแปร startHealth

        Debug.Log($"{name} respawn at {targetPos}");
    }

    protected virtual void Die()
    {
        Debug.Log($"{this.name} is death");
        StartCoroutine(RespawnRoutine());
    }

    private void Update()
    {
        CheckGround();
        HandleDash(); 
    }

    private void HandleDash()
    {
        if (!isDashing) return;

        dashTimer -= Time.deltaTime;
        rb.linearVelocity = new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y);

        if (dashTimer <= 0f)
        {
            isDashing = false;
        }
    }
}
