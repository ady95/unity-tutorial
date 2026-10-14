using UnityEngine;
using UnityEngine.InputSystem;

// WASD·방향키로 플레이어를 8방향 이동시키고, 카메라 화면 밖으로 나가지 않게 한다.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference moveAction; // InputSystem_Actions의 Player/Move
    [SerializeField] float moveSpeed = 5f;            // 유닛/초 (기획서 초깃값)

    Rigidbody2D body;
    Camera mainCamera;
    Vector2 halfSize;
    Vector2 moveInput;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        // 화면 경계 계산에 쓸 플레이어 크기의 절반
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            halfSize = spriteRenderer.bounds.extents;
    }

    void OnEnable() => moveAction.action.Enable();

    void OnDisable() => moveAction.action.Disable();

    void Update()
    {
        // 대각선 입력도 길이가 1을 넘지 않게 해서 같은 속도로 움직이게 한다.
        moveInput = Vector2.ClampMagnitude(moveAction.action.ReadValue<Vector2>(), 1f);
    }

    void FixedUpdate()
    {
        Vector2 target = body.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        body.MovePosition(ClampToScreen(target));
    }

    Vector2 ClampToScreen(Vector2 position)
    {
        Vector2 min = mainCamera.ViewportToWorldPoint(Vector3.zero);
        Vector2 max = mainCamera.ViewportToWorldPoint(Vector3.one);

        position.x = Mathf.Clamp(position.x, min.x + halfSize.x, max.x - halfSize.x);
        position.y = Mathf.Clamp(position.y, min.y + halfSize.y, max.y - halfSize.y);
        return position;
    }
}
