using UnityEngine;

// 화면 바로 바깥 네 방향의 무작위 위치에서 일정 간격마다 적을 하나씩 만든다.
// 시간이 지날수록 생성 간격이 짧아지고 새로 만드는 적이 빨라진다.
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnInterval = 1.5f; // 시작 생성 간격(초) (기획서 초깃값)
    [SerializeField] float edgeOffset = 0.5f;    // 화면 가장자리에서 적 중심까지의 거리. 적 크기의 절반보다 커야 화면 밖에서 나타난다.

    [Header("난이도 (기획서 초깃값)")]
    [SerializeField] float difficultyStep = 30f;         // 난이도가 오르는 주기(초)
    [SerializeField] float spawnIntervalFactor = 0.9f;   // 주기마다 생성 간격 −10%
    [SerializeField] float minSpawnInterval = 0.3f;
    [SerializeField] float enemySpeedFactor = 1.05f;     // 주기마다 적 속도 +5%
    [SerializeField] float maxEnemySpeed = 4f;

    public float Elapsed { get; private set; }
    public float CurrentSpawnInterval => difficulty.SpawnInterval(Elapsed);
    public float CurrentEnemySpeed => difficulty.EnemySpeed(Elapsed);

    Camera mainCamera;
    Difficulty difficulty;
    float timer;

    void Awake()
    {
        mainCamera = Camera.main;

        // 시작 적 속도는 적 프리팹에 설정된 값을 쓴다.
        float baseEnemySpeed = enemyPrefab.GetComponent<EnemyChaser>().MoveSpeed;
        difficulty = new Difficulty(spawnInterval, baseEnemySpeed, difficultyStep,
            spawnIntervalFactor, minSpawnInterval, enemySpeedFactor, maxEnemySpeed);
    }

    void Update()
    {
        Elapsed += Time.deltaTime;
        timer += Time.deltaTime;

        float interval = CurrentSpawnInterval;
        while (timer >= interval)
        {
            timer -= interval;
            Spawn();
        }
    }

    void Spawn()
    {
        var enemy = Instantiate(enemyPrefab, RandomPositionOutsideScreen(), Quaternion.identity);
        enemy.GetComponent<EnemyChaser>().MoveSpeed = CurrentEnemySpeed;
    }

    Vector2 RandomPositionOutsideScreen()
    {
        Vector2 min = mainCamera.ViewportToWorldPoint(Vector3.zero);
        Vector2 max = mainCamera.ViewportToWorldPoint(Vector3.one);

        // 0: 위, 1: 아래, 2: 왼쪽, 3: 오른쪽
        switch (Random.Range(0, 4))
        {
            case 0: return new Vector2(Random.Range(min.x, max.x), max.y + edgeOffset);
            case 1: return new Vector2(Random.Range(min.x, max.x), min.y - edgeOffset);
            case 2: return new Vector2(min.x - edgeOffset, Random.Range(min.y, max.y));
            default: return new Vector2(max.x + edgeOffset, Random.Range(min.y, max.y));
        }
    }
}
