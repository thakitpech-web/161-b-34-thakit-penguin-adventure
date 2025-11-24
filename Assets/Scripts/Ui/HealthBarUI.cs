using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Player player;   // อ้างถึง Player
    [SerializeField] private Slider hpSlider; // Slider ของ HP bar

    private void Start()
    {
        // ถ้าไม่ได้ลาก Player มาใน Inspector ให้ลองหาเอาเองในฉาก
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        if (player != null && hpSlider != null)
        {
            // สมมติว่าเลือดตอนเริ่มคือเลือดเต็ม
            hpSlider.maxValue = player.Health;
            hpSlider.value = player.Health;
        }
    }

    private void Update()
    {
        if (player == null || hpSlider == null) return;

        // อัปเดตค่า Slider ให้ตรงกับเลือดปัจจุบันของ Player
        hpSlider.value = player.Health;
    }
}
