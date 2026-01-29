using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        ScoreManeger.instance.OnScoreChanged += UpdateScore;
        UpdateScore(ScoreManeger.instance.score);
    }

    void UpdateScore(int value)
    {
        scoreText.text = "SCORE : " + value;
    }

    void OnDestroy()
    {
        if (ScoreManeger.instance != null)
        {
            ScoreManeger.instance.OnScoreChanged -= UpdateScore;
        }
    }
}
