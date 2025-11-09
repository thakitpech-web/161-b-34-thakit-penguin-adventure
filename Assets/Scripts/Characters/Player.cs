// Scripts/Characters/Player.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Character
{
    [Header("Jump/Run")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float runMultiplier = 1.5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundRadius = 0.15f;

    [Header("Combat")]
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float attackCooldown = 0.25f;

    private float nextAttackTime;

    private bool IsGrounded =>
        Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

    private void Update()
    {
        // Input
        float x = Input.GetAxisRaw("Horizontal");
        bool run = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetButtonDown("Jump");
        bool fire = Input.GetButtonDown("Fire1");

        // Move (override speed if running)
        float speedBackup = MoveSpeed;
        if (run) typeof(Player).BaseType // just to clarify we use MoveSpeed prop only
            .GetProperty("MoveSpeed");

        // ใช้ Move() จาก base (virtual) + ปรับด้วย multiplier ที่นี่
        float mult = run ? 1.5f : 1f;
        rb.linearVelocity = new Vector2(x * MoveSpeed * mult, rb.linearVelocity.y);
        if (x != 0) Flip(x);

        // Jump
        if (jump && IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Player Jump");
        }

        // Attack
        if (fire && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public override void Attack()
    {
        /*Debug.Log("Player Attack");
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);
        foreach (var h in hits)
        {
            if (h.TryGetComponent<IDamageable>(out var d))
                d.TakeDamage(attackDamage, new Vector2(facingRight ? 2f : -2f, 1.5f));
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
