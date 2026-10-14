using System;

// 경과 시간에 따른 적 생성 간격과 적 속도를 계산한다. Unity API에 의존하지 않아 EditMode 테스트로 바로 검증할 수 있다.
// 주기마다 직전 값에 비율을 곱하고(복리), 한계값을 넘지 않게 자른다.
public class Difficulty
{
    public float BaseSpawnInterval { get; }
    public float BaseEnemySpeed { get; }
    public float StepSeconds { get; }
    public float SpawnIntervalFactor { get; }
    public float MinSpawnInterval { get; }
    public float EnemySpeedFactor { get; }
    public float MaxEnemySpeed { get; }

    public Difficulty(float baseSpawnInterval, float baseEnemySpeed,
        float stepSeconds = 30f,
        float spawnIntervalFactor = 0.9f, float minSpawnInterval = 0.3f,
        float enemySpeedFactor = 1.05f, float maxEnemySpeed = 4f)
    {
        if (baseSpawnInterval <= 0f)
            throw new ArgumentOutOfRangeException(nameof(baseSpawnInterval), "생성 간격은 0보다 커야 합니다.");
        if (baseEnemySpeed < 0f)
            throw new ArgumentOutOfRangeException(nameof(baseEnemySpeed), "적 속도는 음수일 수 없습니다.");
        if (stepSeconds <= 0f)
            throw new ArgumentOutOfRangeException(nameof(stepSeconds), "난이도 주기는 0보다 커야 합니다.");
        if (minSpawnInterval <= 0f)
            throw new ArgumentOutOfRangeException(nameof(minSpawnInterval), "최소 생성 간격은 0보다 커야 합니다.");

        BaseSpawnInterval = baseSpawnInterval;
        BaseEnemySpeed = baseEnemySpeed;
        StepSeconds = stepSeconds;
        SpawnIntervalFactor = spawnIntervalFactor;
        MinSpawnInterval = minSpawnInterval;
        EnemySpeedFactor = enemySpeedFactor;
        MaxEnemySpeed = maxEnemySpeed;
    }

    // 지금까지 난이도가 몇 번 올랐는지. 0초부터 주기 직전까지는 0이다.
    public int Level(float elapsed) => elapsed <= 0f ? 0 : (int)Math.Floor(elapsed / StepSeconds);

    public float SpawnInterval(float elapsed) =>
        (float)Math.Max(MinSpawnInterval, BaseSpawnInterval * Math.Pow(SpawnIntervalFactor, Level(elapsed)));

    public float EnemySpeed(float elapsed) =>
        (float)Math.Min(MaxEnemySpeed, BaseEnemySpeed * Math.Pow(EnemySpeedFactor, Level(elapsed)));
}
