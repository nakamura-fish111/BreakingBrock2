using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text gameOverText;

    private bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isGameOver) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;
        gameOverText.gameObject.SetActive(true);
    }
}
