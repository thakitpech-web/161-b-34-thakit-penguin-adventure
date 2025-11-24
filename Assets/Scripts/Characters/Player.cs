
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
        Point += value;
       
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

    protected virtual void Die()
    {
        Debug.Log($"{this.name} is death");
        Destroy(this.gameObject);
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
