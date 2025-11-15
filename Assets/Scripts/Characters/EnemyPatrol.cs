using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : Enemy
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDirection = 1;

    private int currentDiaection;
    private float halfWidth;
    private Vector2 movement;
        
    private void Start()
    {
        base.Intialize(20);
        DamageHit = 20;

        halfWidth = spriteRenderer.bounds.extents.x;
        currentDiaection = startDirection;
    }

    protected override void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        movement.x = speed * currentDiaection;
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
        SetDiraction();

    }

    [System.Obsolete]
    private void SetDiraction()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) &&
            rb.velocity.x > 0) 
        {
            currentDiaection *= -1;
        }
        else if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground"))
                && rb.velocity.x < 0)
        {
            currentDiaection *= -1;
        }
        Debug.DrawRay(transform.position, Vector2.right * (halfWidth + 0.1f), Color.red);
        Debug.DrawRay(transform.position, Vector2.left * (halfWidth + 0.1f), Color.red);
    }


    public override void Attack()
    {
       
    }

    
}
