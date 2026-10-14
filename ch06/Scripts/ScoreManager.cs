using UnityEngine;

// 생존 시간을 세고 적 처치를 점수에 더한다. 화면 표시 전까지는 일정 간격으로 콘솔에 상태를 출력한다.
public class ScoreManager : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] int pointsPerKill = 10;   // 기획서 초깃값
    [SerializeField] int pointsPerSecond = 1;  // 기획서 초깃값
    [SerializeField] float logInterval = 5f;   // 콘솔 출력 간격(초)

    public Score Score { get; private set; }

    float nextLogTime;

    void Awake()
    {
        Score = new Score(pointsPerKill, pointsPerSecond);
        nextLogTime = logInterval;
    }

    void OnEnable() => EnemyHealth.Died += OnEnemyDied;

    void OnDisable() => EnemyHealth.Died -= OnEnemyDied;

    void Update()
    {
        Score.AddSurvivalTime(Time.deltaTime);

        if (Score.SurvivalTime >= nextLogTime)
        {
            nextLogTime += logInterval;
            LogStatus();
        }
    }

    void OnEnemyDied(EnemyHealth enemy) => Score.AddKill();

    void LogStatus()
    {
        Health health = playerHealth.Health;
        Debug.Log($"[상태] 체력 {health.Current}/{health.Max} · 점수 {Score.Total} (처치 {Score.Kills}) · 생존 시간 {Score.SurvivalTime:F1}초");
    }
}
