using System;

// 체력과 피해 후 무적 시간을 계산한다. Unity API에 의존하지 않아 EditMode 테스트로 바로 검증할 수 있다.
public class Health
{
    public int Max { get; }
    public int Current { get; private set; }
    public float InvulnerabilityDuration { get; }

    float invulnerableUntil = float.NegativeInfinity;

    public Health(int max, float invulnerabilityDuration)
    {
        if (max <= 0)
            throw new ArgumentOutOfRangeException(nameof(max), "최대 체력은 0보다 커야 합니다.");
        if (invulnerabilityDuration < 0f)
            throw new ArgumentOutOfRangeException(nameof(invulnerabilityDuration), "무적 시간은 음수일 수 없습니다.");

        Max = max;
        Current = max;
        InvulnerabilityDuration = invulnerabilityDuration;
    }

    // time 시각에 무적 상태인지 여부. 시각은 호출하는 쪽이 넘긴다(게임에서는 Time.time).
    public bool IsInvulnerable(float time) => time < invulnerableUntil;

    // 피해를 주고, 실제로 체력이 줄었으면 true를 돌려준다.
    // 무적 중이거나 이미 체력이 0이면 피해를 무시한다. 체력은 0 아래로 내려가지 않는다.
    public bool TakeDamage(int amount, float time)
    {
        if (amount <= 0 || Current <= 0 || IsInvulnerable(time))
            return false;

        Current = Math.Max(0, Current - amount);
        invulnerableUntil = time + InvulnerabilityDuration;
        return true;
    }
}
