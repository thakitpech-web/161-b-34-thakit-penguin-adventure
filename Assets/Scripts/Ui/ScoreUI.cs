using UnityEngine;
using TMPro;   // ใช้กับ Text ธรรมดา

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Player player; // อ้างถึง Player
    [SerializeField] private TextMeshProUGUI scoreText; // UI Text ที่เอาไว้โชว์คะแนน

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {
        // อัปเดตทุกเฟรม (ง่ายสุด)
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (player == null || scoreText == null) return;

        scoreText.text = "Score : " + player.Score.ToString();
    }
}
