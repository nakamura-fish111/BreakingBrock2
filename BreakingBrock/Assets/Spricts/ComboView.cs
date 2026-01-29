using TMPro;
using UnityEngine;

public class ComboView : MonoBehaviour
{
    public TMP_Text comboText;

    void Start()
    {
        ScoreManeger.instance.OnComboChanged += UpdateCombo;
        UpdateCombo(0);
    }

    void UpdateCombo(int combo)
    {
        comboText.text = combo > 1 ? combo + " COMBO!" : "";
    }

    void OnDestroy()
    {
        if (ScoreManeger.instance != null)
            ScoreManeger.instance.OnComboChanged -= UpdateCombo;
    }
}
