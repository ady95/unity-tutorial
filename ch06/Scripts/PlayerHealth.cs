using UnityEngine;

// 적과 닿아 있는 동안 접촉 피해를 받는다. 체력과 무적 시간 계산은 Health에 맡긴다.
[RequireComponent(typeof(Collider2D))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;               // 기획서 초깃값
    [SerializeField] int contactDamage = 10;            // 적과 닿았을 때 피해
    [SerializeField] float invulnerabilityDuration = 0.5f; // 피해 후 무적 시간(초)

    public Health Health { get; private set; }

    // 체력이 0이 되었을 때 한 번 알린다.
    public event System.Action Died;

    void Awake()
    {
        Health = new Health(maxHealth, invulnerabilityDuration);
    }

    // 닿은 순간뿐 아니라 계속 붙어 있는 동안에도 호출되므로,
    // 적이 떨어지지 않으면 무적 시간이 끝날 때마다 다시 피해를 받는다.
    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent<EnemyChaser>(out _))
            return;

        // 체력이 0이면 피해를 무시하므로, 0이 되는 피해에서만 한 번 알린다.
        if (Health.TakeDamage(contactDamage, Time.time) && Health.Current <= 0)
            Died?.Invoke();
    }
}
