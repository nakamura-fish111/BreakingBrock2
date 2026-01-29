using UnityEngine;

public class BottomWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {

        AudioManager.instance.PlayRandomSE(
            AudioManager.instance.gameOverSEs
        );

        if (!other.CompareTag("Ball")) return;



        GameManager.instance.GameOver();
        Destroy(other.gameObject);
    }
}
