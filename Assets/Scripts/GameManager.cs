using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int maxScore = 100;
    public static string endReason = "";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score >= maxScore)
        {
            endReason = "win";
            SceneManager.LoadScene("EndScene");
        }
    }

    public void GameOver()
    {
        endReason = "dead";
        SceneManager.LoadScene("EndScene");
    }
}
