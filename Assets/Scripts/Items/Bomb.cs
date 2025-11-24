using UnityEngine;

public class Bomb : Item
{
    [SerializeField] private float knockbackForce = 8f;
    public override void Use(Player player)
    {
        if(player)
        {
            player.takeDamage(ItemValue);
            // หา direction จากระเบิด → ผู้เล่น
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // ดึง rb ของผู้เล่น
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            // รีเซ็ตความเร็วกันวิ่งสวน
            rb.linearVelocity = Vector2.zero;

            // ใส่แรงกระแทกแบบระเบิด
            rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
