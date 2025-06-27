# OverOver

Unity C#으로 제작한 탑다운 화물 밀기 게임 프로토타입입니다.

## 게임 소개

오버워치의 화물 운송 모드를 영감으로 받아 제작한 1대1 전략 게임입니다.

### 게임 규칙

- **공격팀**: 유닛들을 조종하여 화물을 파괴하거나 수비 유닛을 모두 제거
- **수비팀**: 유닛들을 조종하여 화물을 목적지까지 안전하게 호송

### 승리 조건

**공격팀 승리**:
- 화물의 체력을 0으로 만들기
- 수비팀의 모든 유닛 제거

**수비팀 승리**:
- 화물이 목적지 도달
- 공격팀의 모든 유닛 제거

## 주요 기능

- 탑다운 뷰 실시간 전략 게임플레이
- 화물 경로 시스템
- 유닛 선택 및 이동 명령
- 자동 전투 시스템
- 팀별 승리 조건

## 개발 환경

- Unity 2021.3 LTS 이상
- C#

## 설치 방법

1. 이 repository를 clone합니다


```bash
git clone https://github.com/PleaseNoHomework/overover.git
```

2. Unity Hub에서 프로젝트를 엽니다
3. 필요한 패키지들이 자동으로 설치됩니다


# 프로젝트 구조

Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   └── UIManager.cs
│   ├── Units/
│   │   ├── Unit.cs
│   │   ├── AttackerUnit.cs
│   │   └── DefenderUnit.cs
│   ├── Gameplay/
│   │   ├── Payload.cs
│   │   └── PlayerController.cs
│   └── ...
├── Prefabs/
├── Materials/
└── ...

# 플레이 방법

1. 수비팀 플레이어는 유닛을 선택하여 이동 명령 가능
2. 공격팀 유닛은 자동으로 화물을 추적하여 공격
3. 화물은 공격팀 유닛이 근처에 있을 때만 이동
