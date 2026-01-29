using UnityEngine;
using System;

public class ScoreManeger : MonoBehaviour
{
    public static ScoreManeger instance;

    public int score;
    public int combo = 0;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnComboChanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ブロック破壊時に呼ぶ
    public void AddScoreWithCombo(int baseScore)
    {
        combo++;
        int add = baseScore * combo;
        score += add;

        OnComboChanged?.Invoke(combo);
        OnScoreChanged?.Invoke(score);
    }

    // パドルに当たったら呼ぶ
    public void ResetCombo()
    {
        if (combo == 0) return;

        combo = 0;
        OnComboChanged?.Invoke(combo);
    }

    public void ResetAll()
    {
        score = 0;
        combo = 0;
        OnScoreChanged?.Invoke(score);
        OnComboChanged?.Invoke(combo);
    }
}
