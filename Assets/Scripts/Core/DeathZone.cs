using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // เช็คว่าอันที่มาตกใส่คือ Player ไหม (ดูจาก Tag)
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                // ฆ่าทันที: หักเลือดเท่ากับเลือดปัจจุบัน
                player.takeDamage(player.Health);

                Debug.Log("Player ตกแมพ -> ตาย");
            }
        }
    }
}
