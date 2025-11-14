using UnityEngine;

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
    private float dashDirection;   // -1 หรือ 1

    void Start()
    {
        base.Intialize(100);
    }

    // ไม่ต้อง override Awake ก็ได้ แต่ถ้าอยากเพิ่มอะไรค่อยเพิ่มแล้วเรียก base.Awake()
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
        if (isDashing) return; // ขณะ dash ไม่ใช้ Move ปกติ

        base.Move(input);      // ใช้วิธี Move เดิมจาก Character
    }

    public override void Attack()
    {
        // Player เกมนี้ไม่มีอาวุธโจมตี
        // จะปล่อยว่าง หรือใช้เป็นท่าเล็กๆก็ได้
    }

    public void TryDash(float direction)
    {
        if (Time.time < nextDashTime || isDashing) return;

        isDashing = true;
        dashTimer = dashDuration;
        dashDirection = Mathf.Sign(direction == 0 ? transform.localScale.x : direction);
        nextDashTime = Time.time + dashCooldown;
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
