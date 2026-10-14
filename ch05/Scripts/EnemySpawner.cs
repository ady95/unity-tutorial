using UnityEngine;

// 화면 바로 바깥 네 방향의 무작위 위치에서 일정 간격마다 적을 하나씩 만든다.
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnInterval = 1.5f; // 초 (기획서 초깃값)
    [SerializeField] float edgeOffset = 0.5f;    // 화면 가장자리에서 적 중심까지의 거리. 적 크기의 절반보다 커야 화면 밖에서 나타난다.

    Camera mainCamera;
    float timer;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;
        while (timer >= spawnInterval)
        {
            timer -= spawnInterval;
            Instantiate(enemyPrefab, RandomPositionOutsideScreen(), Quaternion.identity);
        }
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
