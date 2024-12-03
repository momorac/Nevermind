# Nevermind

### INDEX
&emsp;[1. 프로젝트 소개](#1-프로젝트-소개)

&emsp;[2. 기술 스택 및 구현](#2-기술-스택-및-구현)

&emsp;[3. 기획 의도 및 과정](#3-기획-의도-및-과정)   

&emsp;[4. 비주얼 및 영상](#4-비주얼-및-영상)  

<br/>

## 1. 프로젝트 소개
### 1-1. 제작자
- **김나은**   (Solo Project)
- Sejong University Design Innovation / Computer Science  
- at 2024 Degital Media Project   

### 1-2. 개발 기간
- 2024.03 ~ 2024.11
- ~ 리팩토링 및 업데이트

### 1-3. 게임 개요
- **장르**: 어드벤처, 캐주얼, 퍼즐, 3D, 3인칭
- **플랫폼**: PC
- **스토리**: 평화롭던 우주의 한 행성. 어느 날 스파크들이 찾아와 행성은 춥고 황폐한 환경이 되었다. <br/>플레이어는 수리공을 도와 얼음을 부시고 가로등을 수리해서 행성을 구해야 한다.

<br/>


## 2. 기술 스택 및 구현
**사용 기술**
- Unity Engine
- Coroutine
- Unity Cinemachine
- Unity AI NavMesh
- Unity Animator & Timeline
- Obi Physics Engine
- DOTween
- Singleton Pattern


### 2-1. 메인 씬 구현
<img src = "https://private-user-images.githubusercontent.com/149387578/391823420-174c40c3-8c72-4b5c-8d41-91015aa3b334.jpg?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MzMyMDM4MDUsIm5iZiI6MTczMzIwMzUwNSwicGF0aCI6Ii8xNDkzODc1NzgvMzkxODIzNDIwLTE3NGM0MGMzLThjNzItNGI1Yy04ZDQxLTkxMDE1YWEzYjMzNC5qcGc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjQxMjAzJTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI0MTIwM1QwNTI1MDVaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT0yOGNhNjQ3MmQ5YTdiN2Q4YmRhMjA2ODFiMTllYzFmODgwNWFmZjZlMmQ4ZGU4ZGRhOWZjNWJlZTY4NTA4ZTE3JlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.HxuVcb16x7E7IbUpVlDtxmzFKHHWRDaC_fViswlH57M">

1. **메인 게임 진행 : 플레이어는 캐릭터를 조작해 얼음 수정들을 부시고, 그에 따라 해금되는 가로등 수리 퍼즐을 풀어야 한다.**
    - 메인 게임과 퍼즐 게임 사이를 오가며 플레이하며, 메인 게임에서의 스탯들을 저장해야 하기 때문에, MainScene에서
    GameManager의 변수들을 static으로 관리하였다.

2. **플레이어블 캐릭터 조작**
    - 행성의 중심방향으로 중력이 작용하기 때문에, rigidbody의 중력을 사용하지 않고, **가상의 Gravity Attracter와 Body** 스크립트를 만들어서 사용
    - Idle, Walk, Jump, Spin, Attack을 상태로 가지는 animator를 통해 캐릭터 모션 컨트롤
    
    ‼️캐릭터를 중심으로 카메라 시점이 전환되기 때문에 y축 시점 회전 시 캐릭터가 제자리에서 미끄러지듯이 어색하게 보이는 문제 발생
    <br>
    → avatar mask 설정하여 animator에서 Bottom Layer 추가, 마우스 회전 입력 시 제자리에서 걸음하는 애니메이션을 override하여 자연스럽게 모션 수정
    

3. **오브젝트 상호작용**
    1. **Crystal** : Collider 기반으로 `CrystalDestroter`스크립트에서 충돌 감지 및 파괴 액션 수행
    2. **Spark** : `SparkSpawner.cs`에서 랜덤하게 주기적으로 에너미 캐릭터 *스파크* 생성, `CrystalDestroyer.cs` 에서 랜덤 이벤트로 에너미 캐릭터 생성
        - 에너미 생성 시 UI 기반으로 에너미 캐릭터의 말풍선 생성
    3. **Light** : 
    4. **Moon** : 달은 플레이어의 진행을 도와주는 캐릭터로,

### 2-2. 퍼즐 씬 구현

### 2-3. 엔딩 씬 구현

<br/>

## 3. 기획 의도 및 과정

<br/>

## 4. 비주얼 및 영상
https://www.youtube.com/watch?v=9vdp6t62SyQ
