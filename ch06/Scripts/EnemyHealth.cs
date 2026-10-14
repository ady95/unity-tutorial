using UnityEngine;

// 적의 체력. 탄환에 맞아 체력이 0이 되면 사라진다. 체력 계산은 Health에 맡긴다.
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 1; // 기획서 초깃값

    // 적이 처치될 때 한 번 알린다.
    public static event System.Action<EnemyHealth> Died;

    public Health Health { get; private set; }

    // 이 프로젝트는 Play 모드에 들어갈 때 도메인 리로드를 하지 않으므로, 이전 실행의 구독자를 직접 지운다.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() => Died = null;

    void Awake()
    {
        // 적은 피해 후 무적 시간이 없다.
        Health = new Health(maxHealth, 0f);
    }

    public void TakeDamage(int amount)
    {
        // 이미 체력이 0인 적은 피해를 무시하므로, 같은 적이 두 번 처치로 세지지 않는다.
        if (!Health.TakeDamage(amount, Time.time) || Health.Current > 0)
            return;

        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
