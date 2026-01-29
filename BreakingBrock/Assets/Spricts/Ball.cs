using UnityEngine;

public class Ball : MonoBehaviour
{
    public float startSpeed = 5f;
    public float minSpeed = 4f;
    public float maxSpeed = 12f;
    public float speedUpRate = 0.5f;

    public float minYRatio = 0.3f;

    private bool isStarted = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        if (!isStarted && Input.GetKeyDown(KeyCode.Space))
        {
            isStarted = true;
            Vector2 dir = new Vector2(1f, 1f).normalized;
            rb.linearVelocity = dir * startSpeed;
        }
    }

    void FixedUpdate()
    {
        if (!isStarted) return;

        Vector2 v = rb.linearVelocity;
        float speed = v.magnitude;

        // 加速
        speed += speedUpRate * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        // Y成分が小さすぎたら補正
        float minY = speed * minYRatio;
        if (Mathf.Abs(v.y) < minY)
        {
            float signY = Mathf.Sign(v.y == 0 ? 1 : v.y);
            v.y = signY * minY;

            // Xは残りで再計算
            float x = Mathf.Sqrt(speed * speed - v.y * v.y);
            v.x = Mathf.Sign(v.x) * x;
        }

        rb.linearVelocity = v.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (AudioManager.instance != null)
        {
            // 衝突先タグでSEを分ける
            switch (collision.gameObject.tag)
            {
                case "Block":
                    AudioManager.instance.PlayRandomSE(AudioManager.instance.hitBlockSEs);
                    break;
                case "Paddle":
                    AudioManager.instance.PlayRandomSE(AudioManager.instance.hitPaddleSEs);
                    ScoreManeger.instance.ResetCombo();
                    BrockSponer.instance.TryRespawnByPaddleHit();
                    break;
                case "Wall":
                    AudioManager.instance.PlayRandomSE(AudioManager.instance.hitBlockSEs);
                    break;
            }
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {

            ScoreManeger.instance.ResetCombo();
            BrockSponer.instance.TryRespawnByPaddleHit();
        }

        ClampSpeedAndAngle();
    }

    void ClampSpeedAndAngle()
    {
        Vector2 v = rb.linearVelocity;
        float speed = Mathf.Clamp(v.magnitude, minSpeed, maxSpeed);

        float minY = speed * minYRatio;
        if (Mathf.Abs(v.y) < minY)
        {
            float signY = Mathf.Sign(v.y == 0 ? 1 : v.y);
            v.y = signY * minY;

            float x = Mathf.Sqrt(speed * speed - v.y * v.y);
            v.x = Mathf.Sign(v.x) * x;
        }

        rb.linearVelocity = v.normalized * speed;
    }
}
