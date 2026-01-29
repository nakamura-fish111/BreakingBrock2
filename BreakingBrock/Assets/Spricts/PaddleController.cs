using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 8f;
    public float bounceStrength = 6f;

    Rigidbody2D rb;
    BoxCollider2D col;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");

        Vector2 pos = rb.position;
        pos.x += x * speed * Time.fixedDeltaTime;

        pos.x = Mathf.Clamp(pos.x, -3.0f, 3.0f);

        rb.MovePosition(pos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (ballRb == null) return;

        float hitPos = collision.transform.position.x - transform.position.x;
        float halfWidth = col.size.x * 0.5f;

        float normalizedHit = hitPos / halfWidth;

        Vector2 dir = new Vector2(normalizedHit, 1f).normalized;
        ballRb.linearVelocity = dir * bounceStrength;
    }
}
