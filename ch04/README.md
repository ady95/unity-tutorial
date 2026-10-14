# 4장 실습 자료 — AI로 게임 오브젝트와 씬 자동 생성하기

책: [4장](https://wikidocs.net/book/21663)

## 04-2. CLI 명령으로 직접 해 보기 (Windows PowerShell 5.1)

`C:\UnityProjects\MyFirstGame`에서, Unity Editor가 열린 상태로 실행합니다. 연습용 씬에서만 작업하고 마지막에 지웁니다.

```powershell
unity command create_scene --path Scenes/Practice --result-only
unity command create_gameobject --name Enemy --result-only
unity command add_component --target /Enemy --type SpriteRenderer --result-only
unity command set_component_properties --target /Enemy --type SpriteRenderer --properties '{\"m_Color\":[1,0,0,1]}' --result-only
unity command set_transform --target /Enemy --position '[3,2,0]' --scale '[0.8,0.8,1]' --result-only
unity command create_prefab --source /Enemy --path Prefabs/Enemy --result-only
unity command instantiate_prefab --prefab Assets/Prefabs/Enemy.prefab --name Enemy2 --result-only

$ops = '[{\"command\":\"instantiate_prefab\",\"params\":{\"prefab\":\"Assets/Prefabs/Enemy.prefab\",\"name\":\"Enemy3\"}},{\"command\":\"set_transform\",\"params\":{\"target\":\"$0.instanceId\",\"position\":[-3,-2,0]}}]'
unity command batch --operations $ops --result-only

# 정리 (04-3에서 Claude가 같은 위치에 진짜 프리팹을 만들므로 반드시 지웁니다)
unity command open_scene --path Assets/Scenes/Game.unity --result-only
unity command delete_asset --asset Assets/Scenes/Practice.unity --confirm true --result-only
unity command delete_asset --asset Assets/Prefabs --confirm true --result-only
git status --short
```

## 04-3. 적 프리팹 만들기 (Claude Code 프롬프트)

새 Claude Code 세션(권한 모드 manual)에서 입력합니다.

```text
기획서 2단계에서 쓸 적 프리팹을 미리 준비해 주세요. 작업 전에 이 프로젝트의 CLAUDE.md에서 이번 작업과 관련된 규칙을 세 줄로 요약해 먼저 알려 주세요.

- Assets/Prefabs/Enemy.prefab 으로 만듭니다. 빨간색 사각형이고, 크기는 플레이어의 0.8배입니다.
- 프리팹 파일만 만들고 Game 씬에는 두지 않습니다. 스크립트나 충돌 처리는 아직 넣지 마세요.

다 만들면 프리팹을 임시로 Game 씬에 하나 꺼내 화면을 캡처해 모양과 색을 확인하세요.
확인 후 임시로 꺼낸 것은 지우고, Game 씬이 작업 전과 같은지 확인한 뒤 "적 프리팹 추가" 메시지로 커밋해 주세요.
```
