using UnityEngine;

// 정해진 방향으로 곧게 날아가 처음 맞은 적에게 피해를 주고 사라진다. 화면 밖으로 나가도 사라진다.
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;     // 유닛/초 (기획서에 없어 정한 값)
    [SerializeField] int damage = 1;        // 기획서 초깃값
    [SerializeField] float screenMargin = 0.5f; // 화면 가장자리에서 이만큼 더 나가면 지운다

    Rigidbody2D body;
    Camera mainCamera;
    bool hasHit;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    public void Launch(Vector2 direction)
    {
        body.linearVelocity = direction.normalized * speed;
    }

    void Update()
    {
        Vector2 min = mainCamera.ViewportToWorldPoint(Vector3.zero);
        Vector2 max = mainCamera.ViewportToWorldPoint(Vector3.one);
        Vector2 position = transform.position;

        if (position.x < min.x - screenMargin || position.x > max.x + screenMargin ||
            position.y < min.y - screenMargin || position.y > max.y + screenMargin)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 같은 물리 단계에서 적 둘에 동시에 닿아도 한 마리에게만 피해를 준다.
        if (hasHit || !other.TryGetComponent<EnemyHealth>(out var enemy))
            return;

        hasHit = true;
        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
