using UnityEngine;

// 플레이어를 향해 직선으로 일정한 속도로 다가간다.
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f; // 유닛/초 (기획서 초깃값)

    Rigidbody2D body;
    Transform target;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        var player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
            target = player.transform;
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        // 플레이어 위치에 닿으면 그 자리에서 멈춘다(지나쳐서 떨리지 않게).
        Vector2 next = Vector2.MoveTowards(body.position, target.position, moveSpeed * Time.fixedDeltaTime);
        body.MovePosition(next);
    }
}
