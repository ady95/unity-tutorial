# 5장 실습 자료 — Claude Code로 게임 플레이 기능 구현하기

책: [5장](https://wikidocs.net/book/21663)

| 파일 | 내용 |
|---|---|
| [Scripts/PlayerMovement.cs](Scripts/PlayerMovement.cs) | 05-2에서 Claude Code가 쓴 플레이어 이동 (Input System Move 액션, Kinematic Rigidbody2D, 화면 경계) |
| [Scripts/EnemySpawner.cs](Scripts/EnemySpawner.cs) | 05-4의 적 생성기 (1.5초마다 화면 바로 바깥 네 방향) |
| [Scripts/EnemyChaser.cs](Scripts/EnemyChaser.cs) | 05-5의 적 추적 (2 유닛/초, Vector2.MoveTowards) |

스크립트만으로는 동작하지 않습니다. Player에 Rigidbody2D와 PlayerMovement를 붙이고 Move 액션을 연결하는 등 씬·프리팹 설정은 Claude Code가 Editor에서 한 작업입니다. AI의 결과는 실행할 때마다 다르므로, 내 스크립트와 비교하는 용도로 쓰세요.

## 실습 프롬프트

같은 Claude Code 세션에서 순서대로 입력합니다. 저자는 auto 모드로 측정했고, manual 모드로 해도 됩니다.

### 05-2. 플레이어 이동

```text
기획서 2단계 중 플레이어 이동을 구현해 주세요.

- WASD와 방향키로 8방향 이동하고, 대각선도 같은 속도로 움직입니다. 이동 속도는 기획서의 값을 씁니다.
- 입력은 프로젝트에 있는 InputSystem_Actions의 Move 액션을 씁니다.
- Rigidbody2D로 움직이고, 플레이어가 카메라 화면 밖으로 나가지 않게 합니다.
- 스크립트는 Assets/Scripts/PlayerMovement.cs 하나로 만듭니다. 적은 아직 건드리지 마세요.

구현이 끝나면 Play 모드에서 확인해 주세요.
- Play 모드는 unity-cli Skill의 Play 모드 검증 절차대로 들어가고, 프레임이 실제로 진행되는지 먼저 확인합니다.
- 키 입력은 unity command simulate_key 로 넣고, 움직였는지는 Player의 위치 값을 읽어 판단합니다.
- 오른쪽으로 계속 이동했을 때 화면 끝에서 멈추는지 확인하고, 그 화면을 캡처해 보여 주세요.
확인이 끝나면 Play 모드를 끄고 바꾼 설정을 원래대로 되돌린 뒤 "플레이어 이동" 메시지로 커밋해 주세요.
```

### 05-4. 적 생성기

```text
기획서 2단계 중 적 생성을 구현해 주세요.

- Enemy 프리팹을 화면 바로 바깥 네 방향의 무작위 위치에서, 기획서의 생성 간격마다 하나씩 만듭니다.
- Game 씬에 EnemySpawner 오브젝트를 두고, 스크립트는 Assets/Scripts/EnemySpawner.cs 로 만듭니다.
- 적은 아직 움직이지 않습니다. 추적은 다음 단계에서 만듭니다.

Play 모드에서 확인해 주세요.
- 게임 시간 약 7초가 되면 일시정지하도록 wait_for 로 멈춘 뒤, 적의 개수와 위치 값을 읽어
  생성 간격대로 늘었는지와 모두 화면 바로 바깥에 있는지 판단합니다.
확인이 끝나면 Play 모드를 끄고 바꾼 설정을 원래대로 되돌린 뒤 "적 생성기" 메시지로 커밋해 주세요.
```

### 05-5. 플레이어 추적

```text
생성된 적이 플레이어를 추적하게 해 주세요.

- 적은 플레이어를 향해 직선으로 움직입니다. 속도는 기획서의 값을 씁니다.
- 스크립트는 Assets/Scripts/EnemyChaser.cs 로 만들어 Enemy 프리팹에 붙입니다.
- 충돌과 피해는 아직 만들지 마세요.

Play 모드에서 확인해 주세요.
- 게임 시간 약 7초에 일시정지해 적과 플레이어의 거리를 기록하고 화면을 캡처합니다.
- 1초 정도 더 진행한 뒤 다시 멈춰 거리가 기획서의 속도만큼 줄었는지 확인합니다.
확인이 끝나면 Play 모드를 끄고 바꾼 설정을 원래대로 되돌린 뒤 "적 추적" 메시지로 커밋해 주세요.
```
