using System;

// 적 처치 수와 생존 시간으로 점수를 계산한다. Unity API에 의존하지 않아 EditMode 테스트로 바로 검증할 수 있다.
public class Score
{
    public int PointsPerKill { get; }
    public int PointsPerSecond { get; }

    public int Kills { get; private set; }
    public float SurvivalTime { get; private set; } // 초

    // 생존 점수는 다 채운 1초마다 준다.
    public int SurvivalSeconds => (int)Math.Floor(SurvivalTime);
    public int Total => Kills * PointsPerKill + SurvivalSeconds * PointsPerSecond;

    public Score(int pointsPerKill = 10, int pointsPerSecond = 1)
    {
        if (pointsPerKill < 0)
            throw new ArgumentOutOfRangeException(nameof(pointsPerKill), "처치 점수는 음수일 수 없습니다.");
        if (pointsPerSecond < 0)
            throw new ArgumentOutOfRangeException(nameof(pointsPerSecond), "생존 점수는 음수일 수 없습니다.");

        PointsPerKill = pointsPerKill;
        PointsPerSecond = pointsPerSecond;
    }

    public void AddKill() => Kills++;

    public void AddSurvivalTime(float seconds)
    {
        if (seconds < 0f)
            throw new ArgumentOutOfRangeException(nameof(seconds), "시간은 음수일 수 없습니다.");

        SurvivalTime += seconds;
    }
}
