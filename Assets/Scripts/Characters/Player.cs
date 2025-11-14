
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : Character
{
    [Header("Jump/Run")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float runMultiplier =5.0f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundRadius = 0.15f;

    [Header("Dash")]
    [SerializeField] private float dashForce = 12f;      
    [SerializeField] private float dashDuration = 0.15f; 
    [SerializeField] private float dashCooldown = 1.5f;  
    private bool isDashing = false;
    private float lastDashTime = -999f;

    private bool IsGrounded =>
        Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

    private void Update()
    {
        if (isDashing)
            return;

        float x = Input.GetAxisRaw("Horizontal");
        bool run = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetButtonDown("Jump");
        bool dash = Input.GetMouseButtonDown(1);



        float mult = run ? 1.5f : 1f; //เดิน, วิ่ง
        rb.linearVelocity = new Vector2(x * MoveSpeed * mult, rb.linearVelocity.y);
        if (x != 0) Flip(x);

        
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x)); //อนิเมชันการเดิน
        anim.SetBool("isRun", run); //อนิเมชันการวิ่ง

        // กระโดด
        if (jump && IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Player Jump");
        }

        

        if (dash && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(Dash());
        }



    }

    private IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        
        float dir = Mathf.Sign(transform.localScale.x); //หันทิศตาม localScale.x
        if (dir == 0) dir = 1; 

        float originalGravity = rb.gravityScale;

        
        rb.gravityScale = 0f; //ระหว่าง Dash จะไม่แตะพื้น

        rb.linearVelocity = new Vector2(dir * dashForce, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    public override void Attack()
    {
        
    }

    
}
