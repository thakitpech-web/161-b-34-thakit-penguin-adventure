using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    private int heath;
    public event Action<int, int> OnHealthChanged;
    [SerializeField] public float maxHp = 100.0f;

    public int Heath
    {
        get { return heath; }
        set
        {
            heath = (value < 0) ? 0 : value;
            OnHealthChanged?.Invoke(heath, 100);
        }
    }
    public void Intialize(int startHeath)
    {
        maxHp = startHeath;
        Heath = startHeath;
        OnHealthChanged?.Invoke(Heath, startHeath);
        Debug.Log($"{this.name} is intialize Heath : {this.Heath}");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    [SerializeField] protected float moveSpeed = 3f;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Awake()      // virtual method
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // virtual: ลูกจะ override ถ้าอยากเปลี่ยนวิธีเดิน
    public virtual void Move(Vector2 input)
    {
        rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
        if (anim != null)
            anim.SetFloat("Speed", Mathf.Abs(input.x));

        // พลิกสปรייט
        if (input.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(input.x), 1, 1);
    }

    // abstract: บังคับให้ทุกตัวละครต้องมีการโจมตี/แอคชันหลักของตัวเอง
    public abstract void Attack();

    public virtual void TakeDamage(int amount)
    {
        Heath -= amount;
        if (Heath <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // เล่นแอนิเมชันตายได้ ถ้าขี้เกียจค่อยเพิ่มทีหลัง
        Destroy(gameObject);
    }
}
