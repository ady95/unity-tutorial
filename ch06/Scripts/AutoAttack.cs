using System.Collections.Generic;
using UnityEngine;

// 일정 간격마다 사거리 안의 가장 가까운 적에게 탄환을 발사한다.
public class AutoAttack : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float fireInterval = 0.5f; // 초 (기획서 초깃값)
    [SerializeField] float range = 6f;          // 유닛 (기획서 초깃값)
    [SerializeField] LayerMask enemyLayers;     // 적을 찾을 레이어 (Enemy)

    readonly List<Collider2D> hits = new List<Collider2D>();
    ContactFilter2D enemyFilter;
    float cooldown;

    void Awake()
    {
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(enemyLayers);
        enemyFilter.useTriggers = true; // 적 충돌 영역은 트리거다
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown > 0f)
            return;

        // 사거리 안에 적이 없으면 준비된 상태로 기다렸다가, 적이 들어오는 즉시 쏜다.
        Transform target = FindNearestEnemy();
        if (target == null)
        {
            cooldown = 0f;
            return;
        }

        Fire(target.position);
        cooldown = fireInterval;
    }

    Transform FindNearestEnemy()
    {
        Vector2 origin = transform.position;
        Physics2D.OverlapCircle(origin, range, enemyFilter, hits);

        // OverlapCircle은 충돌 영역 가장자리만 걸쳐도 찾으므로, 사거리는 적의 중심까지 거리로 다시 확인한다.
        Transform nearest = null;
        float nearestSqr = range * range;
        foreach (var hit in hits)
        {
            float sqr = ((Vector2)hit.transform.position - origin).sqrMagnitude;
            if (sqr <= nearestSqr)
            {
                nearestSqr = sqr;
                nearest = hit.transform;
            }
        }
        return nearest;
    }

    void Fire(Vector2 targetPosition)
    {
        Vector2 origin = transform.position;
        Vector2 direction = targetPosition - origin;
        if (direction == Vector2.zero)
            direction = Vector2.up; // 적이 정확히 겹쳐 있으면 아무 방향이나 쏜다(바로 닿는다)

        var projectile = Instantiate(projectilePrefab, origin, Quaternion.identity);
        projectile.Launch(direction);
    }
}
