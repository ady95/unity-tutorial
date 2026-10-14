# unity-tutorial

위키독스 책 **《Unity AI 게임 개발 따라하기》 — Unity CLI와 Claude Code로 게임 만들기**의 실습 자료 저장소입니다.

<a href="https://wikidocs.net/book/21663"><img src="assets/book-cover-v2.png" alt="Unity AI 게임 개발 따라하기 책 표지" width="300"></a>

- 책: https://wikidocs.net/book/21663
- Unity 공식 Claude Code Plugin: https://github.com/Unity-Technologies/unity-agent-plugin

> 집필 중입니다. 장이 공개될 때마다 해당 장의 실습 자료가 추가됩니다.

## 검증 기준

| 항목 | 버전 |
|---|---|
| OS | Windows 11 |
| Unity Editor | 6000.3.26f1 (Unity 6.3 LTS) |
| Unity CLI | 1.0.0-beta.13 |
| Unity Pipeline 패키지 (com.unity.pipeline) | 0.8.0-exp.1 |
| Claude Code | 2.1.296 |
| Unity 공식 Claude Code Plugin (unity@unity-agent-plugin) | 0.1.8-beta |
| 프로젝트 템플릿 | com.unity.template.universal-2d (Universal 2D) |

Unity Hub는 사용하지 않습니다. Editor 설치부터 프로젝트 생성까지 Unity CLI만으로 진행합니다.

책의 명령은 위 버전 조합을 기준으로 실행해 확인합니다. Unity CLI·Pipeline·플러그인은 베타·실험 단계라 다른 버전에서는 명령이나 출력이 다를 수 있습니다.

## 실습 구성

책은 2D 서바이벌 액션 게임 하나를 3장부터 9장까지 단계적으로 완성하고, 10장에서 3D 미니 게임으로 확장합니다.

| 장 | 게임 상태 |
|---|---|
| 3~4장 | 배경과 플레이어만 있는 화면 |
| 5장 | 키보드로 플레이어 이동, 적이 출현해 플레이어를 추적 |
| 6장 | 공격, 충돌, 체력, 점수 |
| 7장 | 메뉴, 효과음, 게임오버, 재시작 |
| 8~9장 | 자동 테스트를 통과한 Windows 실행 파일과 WebGL 빌드 |
| 10장 | 3D 장애물 탈출 게임으로 확장 |

각 장의 실습 프롬프트 원문과 장별 결과물을 함께 제공해, 중간 장부터 시작하거나 내 결과와 비교할 수 있게 합니다.

## 실습 폴더

| 장 | 폴더 | 내용 |
|---|---|---|
| 3장 | [ch03](ch03) | 실습 프롬프트 5개, Claude Code가 쓴 게임 기획서·CLAUDE.md·.gitignore |
| 4장 | [ch04](ch04) | CLI 명령 연습(04-2), 적 프리팹 프롬프트(04-3) |
| 5장 | [ch05](ch05) | 플레이어 이동·적 생성·추적 프롬프트, Claude Code가 쓴 스크립트 3개 |

## 주의

- Claude Code의 결과는 같은 프롬프트라도 실행할 때마다 달라질 수 있습니다. 결과가 다르면 장별 완성본과 비교해 보세요.
- `unity command eval`은 실행 중인 Unity Editor에 임의의 C# 코드를 실행합니다. 로컬 개발 환경에서만 사용하고, 실행 전에 Git 커밋으로 복구 지점을 만들어 두세요.
- 7장 이후에 쓰는 이미지·사운드 에셋은 각 폴더에 출처와 라이선스를 함께 적습니다.

## 라이선스

예제 코드는 [MIT License](LICENSE)를 따릅니다. 외부 에셋은 각 에셋의 라이선스를 따릅니다.
