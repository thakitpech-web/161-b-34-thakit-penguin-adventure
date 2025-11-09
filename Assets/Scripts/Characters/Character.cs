// Scripts/Characters/Character.cs
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable, iAttacker
{
    [Header("Stats")]
    [SerializeField] private int maxHP = 3;
    [SerializeField] private float moveSpeed = 5f;

    public int MaxHP => maxHP;               // Encapsulation (read-only)
    public int CurrentHP { get; private set; }
    public float MoveSpeed => moveSpeed;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected bool facingRight = true;

    protected virtual void Awake()   // virtual ⇒ child can extend
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CurrentHP = maxHP;
    }

    protected virtual void Flip(float dirX)
    {
        if ((dirX > 0 && !facingRight) || (dirX < 0 && facingRight))
        {
            facingRight = !facingRight;
            var s = transform.localScale;
            s.x *= -1;
            transform.localScale = s;
        }
    }

    protected virtual void Move(float dirX)  // virtual for polymorphism
    {
        rb.linearVelocity = new Vector2(dirX * MoveSpeed, rb.linearVelocity.y);
        Flip(dirX);
    }

    public abstract void Attack();           // Abstract method

    public virtual void TakeDamage(int amount)
    {
        CurrentHP = Mathf.Max(0, CurrentHP - amount);
        Debug.Log($"{name} TakeDamage {amount} → HP {CurrentHP}");
        if (CurrentHP <= 0) Die();
    }

    public virtual void TakeDamage(int amount, Vector2 knockback) // Overloading
    {
        TakeDamage(amount);
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    protected virtual void Die()
    {
        Debug.Log($"{name} died");
        Destroy(gameObject);
    }
}
