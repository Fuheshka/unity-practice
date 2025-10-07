using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake() {
        if (instance == null) instance = this;
    }

    public void AddScore(int amount) {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
