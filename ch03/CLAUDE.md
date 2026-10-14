# MyFirstGame 개발 규칙

위에서 내려다보는 2D 서바이벌 액션 게임입니다. Unity 6000.3, URP 2D 렌더러를 씁니다.

## 기획
- 게임 기획은 `docs/GameDesign.md`를 따른다. 단계별 구현 계획의 순서대로 진행한다.

## Editor 조작
- 실행 중인 Unity Editor가 있으면 Unity CLI(`unity command`)로 조작한다. 작업 전에 `unity status`로 연결 상태를 확인한다.
- `.unity`·`.prefab` 파일을 직접 편집하지 않는다.
- `unity command eval`은 상태 확인과 간단한 조작에만 쓴다.
- 큰 변경 전에는 Git 커밋으로 복구 지점을 만든다.

## 파일 위치
- 스크립트는 `Assets/Scripts`, 씬은 `Assets/Scenes`에 둔다.

## 코드
- 입력은 Input System 패키지를 사용한다. 예전 `UnityEngine.Input` API는 쓰지 않는다. Player Settings가 새 Input System만 쓰도록 설정되어 있다.

## 작업 범위
- 한 번에 한 기능만 구현한다.
- 요청 범위 밖의 파일은 수정하지 않는다.

## 확인과 보고
기능을 구현하면 아래를 모두 확인한 뒤 보고한다.
1. 컴파일 오류가 없는지 확인한다.
2. 콘솔 오류가 없는지 확인한다. 경고도 함께 보고한다.
3. 결과 화면을 캡처해 직접 확인한다. 확인용 캡처 파일은 확인 후 프로젝트에서 지운다.

## Unity CLI 사용 메모
- 모든 `unity command` 호출에 `--caller plugin --skill unity-cli`를 붙인다.
- 화면 캡처: `unity command screenshot --view game`. 파일은 `Temp/pipeline-screenshots/`에 저장된다.
- 콘솔 확인: `unity command console_status`(오류·경고 개수, 컴파일 실패 여부), `unity command console --level warning`.
- PowerShell에서 `eval`에 C# 코드를 인수로 넘기면 따옴표가 깨진다. 코드는 스크래치 파일에 써서 `unity command eval_file --file <경로>`로 실행한다.
- `eval`·`eval_file` 코드는 메서드 본문으로 실행되므로 `using` 지시문을 쓸 수 없다. `UnityEngine.GameObject`처럼 전체 이름을 쓴다.
