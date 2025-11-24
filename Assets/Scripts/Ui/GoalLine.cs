using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalLine : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private bool hasWon = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasWon) return;

        if (other.CompareTag("Player"))
        {
            hasWon = true;
            WinGame();
        }
    }

    private void WinGame()
    {
        // เปิดหน้า Win
        if (winPanel != null)
            winPanel.SetActive(true);

        // หยุดเวลาในเกม
        Time.timeScale = 0f;
    }

    // เรียกจากปุ่ม Retry
    public void RetryGame()
    {
        Time.timeScale = 1f; // ปล่อยเวลาให้เดินปกติ
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    // เรียกจากปุ่ม Quit
    public void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
