using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// 플레이어 체력이 0이 되면 게임을 멈추고, 게임오버 상태에서 Enter·Space를 누르면 처음부터 다시 시작한다.
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Behaviour[] stopOnGameOver; // 게임오버 때 끌 컴포넌트(플레이어 이동, 자동 공격, 적 생성, 점수)

    public bool IsGameOver { get; private set; }

    InputAction restartAction;

    void Awake()
    {
        // 게임오버로 멈춘 시간을 새 게임에서 되돌린다.
        Time.timeScale = 1f;

        restartAction = new InputAction("Restart", InputActionType.Button);
        restartAction.AddBinding("<Keyboard>/enter");
        restartAction.AddBinding("<Keyboard>/numpadEnter");
        restartAction.AddBinding("<Keyboard>/space");
    }

    void OnEnable()
    {
        playerHealth.Died += OnPlayerDied;
        restartAction.performed += OnRestartPerformed;
        restartAction.Enable();
    }

    void OnDisable()
    {
        playerHealth.Died -= OnPlayerDied;
        restartAction.performed -= OnRestartPerformed;
        restartAction.Disable();
    }

    void OnDestroy() => restartAction.Dispose();

    // 프레임마다 WasPressedThisFrame()으로 확인하지 않고 입력 이벤트로 받는다.
    // 게임 프레임 밖에서 들어온 입력(예: Editor에서 주입한 키)도 놓치지 않는다.
    void OnRestartPerformed(InputAction.CallbackContext context)
    {
        if (IsGameOver)
            Restart();
    }

    void OnPlayerDied()
    {
        IsGameOver = true;

        // 물리(적·탄환 이동)와 시간에 따라 움직이는 것을 모두 멈추고, 입력·생성·공격 컴포넌트도 끈다.
        Time.timeScale = 0f;
        foreach (var behaviour in stopOnGameOver)
            behaviour.enabled = false;

        Score score = scoreManager.Score;
        Debug.Log($"[게임오버] 점수 {score.Total} (처치 {score.Kills}) · 생존 시간 {score.SurvivalTime:F1}초 — Enter 또는 Space로 다시 시작");
    }

    void Restart()
    {
        // Enter와 Space를 동시에 눌러도 한 번만 다시 시작한다.
        IsGameOver = false;
        Debug.Log("[재시작] 게임을 처음부터 다시 시작합니다.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
