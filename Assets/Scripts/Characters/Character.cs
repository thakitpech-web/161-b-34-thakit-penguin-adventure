using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    
    public event Action<int, int> OnHealthChanged;
    [field: SerializeField] public int Point { get; set; } = 0;
    [field: SerializeField] public int Health { get; set; } = 100;

   
    public void Intialize(int startHeath)
    {
        Health = startHeath;
        OnHealthChanged?.Invoke(Health, startHeath);
        Debug.Log($"{this.name} is intialize Heath : {this.Health}");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    [SerializeField] protected float moveSpeed = 3f;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Awake()      
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    
    public virtual void Move(Vector2 input)
    {
        rb.linearVelocity = new Vector2(input.x * moveSpeed, rb.linearVelocity.y);
        if (anim != null)
            anim.SetFloat("Speed", Mathf.Abs(input.x));

        // พลิกสปรייט
        if (input.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(input.x), 1, 1);
    }

    

 
}
