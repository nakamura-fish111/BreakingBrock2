using UnityEngine;

public class CreakBrock : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
