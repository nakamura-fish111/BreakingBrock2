using UnityEngine;

public class BrockSponer : MonoBehaviour
{
    public static BrockSponer instance;

    [Header("Block Prefabs")]
    public GameObject blockPrefab;          // 初回用（固定）
    public GameObject[] blockPrefabs;        // 2回目以降ランダム

    public int rows = 5;
    public int columns = 10;

    public float blockWidth = 1.2f;
    public float blockHeight = 0.4f;

    public Vector2 startPos = new Vector2(-5.4f, 3.5f);

    private int blockCount = 0;
    private bool canRespawn = false;
    private int spawnCount = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        spawnCount++;
        blockCount = 0;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector2 pos = new Vector2(
                    startPos.x + x * blockWidth,
                    startPos.y - y * blockHeight
                );

                GameObject prefabToSpawn;

                // 1回目は固定、2回目以降はランダム
                if (spawnCount == 1)
                {
                    prefabToSpawn = blockPrefab;
                }
                else
                {
                    prefabToSpawn = blockPrefabs[
                        Random.Range(0, blockPrefabs.Length)
                    ];
                }

                Instantiate(prefabToSpawn, pos, Quaternion.identity);
                blockCount++;
            }
        }
    }

    // ボールがパドルに当たった時に呼ばれる
    public void TryRespawnByPaddleHit()
    {
        if (!canRespawn) return;

        SpawnBlocks();
        canRespawn = false;
    }

    // ブロック破壊時に呼ばれる
    public void OnBlockDestroyed()
    {
        blockCount--;

        if (blockCount <= 0)
        {
            canRespawn = true;
        }
    }
}
