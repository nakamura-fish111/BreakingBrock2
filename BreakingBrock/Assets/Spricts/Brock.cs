using UnityEngine;

public class Brock : MonoBehaviour
{
    [Header("Score")]
    public int scoreValue = 100;

    [Header("Durability")]
    [Min(1)]
    public int hitPoint = 1;

    [Header("Behavior")]
    public bool isThrough = false;
    public bool destroyOnHit = true;

    [Header("Color")]
    public Color maxHpColor = Color.cyan;
    public Color minHpColor = Color.red;
    public Color throughColor = new Color(0.6f, 0f, 0.8f);
    public Color indestructibleColor = Color.black;

    private int maxHitPoint;
    private SpriteRenderer sr;

    private int lastHitFrame = -1;

    void Awake()
    {

        sr = GetComponentInChildren<SpriteRenderer>();

        maxHitPoint = hitPoint;

        UpdateColor();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;
        if (isThrough) return;

        TryHit();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;
        if (!isThrough || !destroyOnHit) return;

        TryHit();
    }

    void TryHit()
    {
        if (lastHitFrame == Time.frameCount) return;
        lastHitFrame = Time.frameCount;

        hitPoint--;



        if (hitPoint <= 0)
        {
            BrockSponer.instance.OnBlockDestroyed();
            DestroyBlock();
        }
        else
        {
            UpdateColor();
        }
    }

    void DestroyBlock()
    {
        ScoreManeger.instance.AddScoreWithCombo(scoreValue);
        BrockSponer.instance.OnBlockDestroyed();
        Destroy(gameObject);
    }

    void UpdateColor()
    {
        if (sr == null) return;

        if (isThrough && !destroyOnHit)
        {
            sr.color = indestructibleColor;
            return;
        }

        if (isThrough)
        {
            sr.color = throughColor;
            return;
        }

        float t = (float)hitPoint / maxHitPoint;
        sr.color = Color.Lerp(minHpColor, maxHpColor, t);
    }
}
