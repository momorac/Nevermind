# Nevermind


<br>

### INDEX
&emsp;[1. 프로젝트 소개](#1-프로젝트-소개)

&emsp;[2. 기술 스택 및 구현](#2-기술-스택-및-구현)

&emsp;[3. 기획 의도 및 제작과정](#3-기획-의도-및-제작과정)   

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

<br>
<br>


## 2. 기술 스택 및 구현
**사용 기술**
- `Unity Engine`
- `Coroutine`
- `Unity Cinemachine`
- `Unity AI NavMesh`
- `Unity Animator & Timeline`
- `Obi Physics Engine`
- `DOTween`
- `Singleton Pattern`

**버전 정보** : Unity 2022.3.16lf

<br>


### 2-1. 메인 씬 구현
<img src = "https://private-user-images.githubusercontent.com/149387578/391823420-174c40c3-8c72-4b5c-8d41-91015aa3b334.jpg?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MzMzNTc5NTIsIm5iZiI6MTczMzM1NzY1MiwicGF0aCI6Ii8xNDkzODc1NzgvMzkxODIzNDIwLTE3NGM0MGMzLThjNzItNGI1Yy04ZDQxLTkxMDE1YWEzYjMzNC5qcGc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjQxMjA1JTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI0MTIwNVQwMDE0MTJaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT1kMzliZmQwZTI4NjIxYTk5MjYyZTViNzMwYTEzZGFmNGUwNGE4NDI1Yjk1MjdkZmRkZGM4YjgxNDAzNzg3MGNkJlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.e1sG9JQQDURKaH7AE4401DE5AXLd3JhetzFWyLjU3T4">

1. **메인 게임 진행 : 플레이어는 캐릭터를 조작해 얼음 수정들을 부시고, 그에 따라 해금되는 가로등 수리 퍼즐을 풀어야 한다.**
    - 여러 씬 사이에서 상태를 통합적으로 관리하기 위해, MainScene에서 **GameManager의 필드를 static으로 관리**하였다.

2. **플레이어블 캐릭터 조작**
    - 행성의 중심방향으로 중력이 작용하기 때문에, rigidbody의 중력을 사용하지 않고, **가상의 Gravity Attracter와 Body** 스크립트를 만들어서 사용
    - Idle, Walk, Jump, Spin, Attack을 상태로 가지는 animator를 통해 캐릭터 모션 컨트롤
    
    ‼️캐릭터를 중심으로 카메라 시점이 전환되기 때문에 y축 시점 회전 시 캐릭터가 제자리에서 미끄러지듯이 어색하게 보이는 문제 발생
    <br>
    → avatar mask 설정하여 animator에서 Bottom Layer 추가, 마우스 회전 입력 시 제자리에서 걸음하는 애니메이션을 override하여 자연스럽게 모션 수정
    

3. **오브젝트 상호작용**
    1. **Crystal** : Collider 기반으로 `CrystalDestroter`스크립트에서 충돌 감지 및 파괴 액션 수행
    2. **Spark** : `SparkSpawner.cs`에서 랜덤하게 주기적으로 에너미 캐릭터 스파크 생성, `CrystalDestroyer.cs` 에서 랜덤 이벤트로 에너미 캐릭터 생성
        - 에너미 생성 시 UI 기반으로 에너미 캐릭터의 말풍선 생성
    3. **Light** : Light 오브젝트는 퍼즐 게임 진입을 알려주는 큐의 역할로, 퍼즐 해금 조건 달성 시 플레이어와 최단거리의 Light 오브젝트 탐색 및 상태 변경
    4. **Moon** : 달은 플레이어의 진행을 도와주는 캐릭터로, Light 오브젝트가 활성화되면 플레이어의 시야로 이동해 가까운 Light로 안내하는 역할 수행
  
<br>
<br>

### 2-2. 퍼즐 씬 구현
1. **플레이어 캐릭터 조작**
   - **NavMesh**를 이용한 ClickMovement 구현
2. **Plug 오브젝트를 획득**
    - Avatar Mask를 통해 Plug를 잡고 있는 상반신의 모션 구현

3. **Plug와 연결된 전선을 Fuze 오브젝트에 접촉**
    - 파티클 기반 [**Obi Physic Engine**](https://obi.virtualmethodstudio.com)의 **Obi Rope**를 사용해 전선의 움직임 구현
    - Obi Engine의 자체 Collider와 Surface를 이용해 전선과 퓨즈의 접촉 상태 감지

4. **접촉된 상태에서 Console 오브젝트에 도착**
    - Coroutine을 이용한 엔딩 애니메이션 구현
    - Cinemachine을 이용해 카메라 시점 전환 연출

<br>

### 2-3. 엔딩 씬 구현
[Click to Watch](https://www.youtube.com/watch?v=9vdp6t62SyQ)

**스크립트로 작성한 Animation Event와 Timeline을 복합적으로 사용하여 구현**

- Cinemachine을 통해 총 4개의 씬에서의 카메라 연출 제어
- Timeline을 통해 오브젝트 기본 애니메이션 제어
- Skybox Material의 **Shader 코드에 접근**, DOTween을 이용해 자연스럽게 변화하는 하늘의 애니메이션 구현
- 재귀 함수를 통해 CreditRoll 구현


<br/>

## 3. 기획 의도 및 제작과정

### 기획 의도
**Menifesto : “내가 진정 하고싶은 이야기”란 ?** 
- 많은 사람들이 답도 없는 문제에 대해 Over-Thinking하며 스스로를 상처입힌다.
- 뾰족뾰족한 Over-Thinking이 아니라, 둥글게 생각하며 Oval-Thinking으로 스스로를 안아주기를 바란다.
  
<br>

### 제작 과정
#### 1. 기획 및 디자인
- 1~2주차 : 기획 및 모티브 선정
- 3주차 : 배경 이미지 스케치 및 게임 플로우 구상
  <image src ="https://file.notion.so/f/f/e8b2a23a-986d-4a45-8ed3-ff45015f9488/e41bb0d4-3fd9-4399-ad31-e50e3d12f7d3/image.png?table=block&id=1534284e-2670-805e-80ac-d751899f4dd4&spaceId=e8b2a23a-986d-4a45-8ed3-ff45015f9488&expirationTimestamp=1733457600000&signature=sMFnMYx4Ryg7KGE-w7DStkNHJfR_mBkSPIwPzeH7j0Q&downloadName=image.png">
- 4~6주차 : 캐릭터 디자인 및 모델링, 리깅
  <image src ="https://file.notion.so/f/f/e8b2a23a-986d-4a45-8ed3-ff45015f9488/485a3661-8c8f-4ab6-927c-59e2e88d97a8/image.png?table=block&id=1534284e-2670-80af-a851-faa92fc75ff4&spaceId=e8b2a23a-986d-4a45-8ed3-ff45015f9488&expirationTimestamp=1733457600000&signature=_J_iHiEkCWbhqMbCjkcECMCdvNtXWhO3gxoyC7lfolU&downloadName=image.png">

#### 2. 개발
- 7~8주차 : 인게임 행성 중력 및 캐릭터 움직임 구현
  
- 9주차 : 배경 오브젝트 모델링 및 배치, Particle 기반 오브젝트 구현
- 10주차 : 게임 시스템 구축 및 키비주얼 제작
    - 플레이어 공격 스킬 구현 및 애니메이터 상태 구축
    - 에너미 캐릭터 생성 및 움직임 구현
    - 수정 파괴 구현
- 11주차 
    - 사용 가능한 아이템 구현
    - HP, 스킬 쿨타임, 아이템 개수를 보여주는 UI 구현
    - 효과음 추가 및 스크립트에서 제어
- 12주차 : 게임 비주얼 리뉴얼 - 배경의 행성과 소품, 톤앤매너까지 다시 디자인 및 모델링, 배치 작업
- 13~14주차
    - UI 개편 및 아이템 사용 방식 변경
    - 아이템 획득, 사용 시 캔버스 기반 이펙트 추가
    - 튜토리얼 구현
    - 컬러무드 수정


<br/>

## 4. 비주얼 및 영상
#### 메인 컨셉 이미지
<image src = "https://private-user-images.githubusercontent.com/149387578/392602590-419a268a-e024-45d8-b095-128244180b39.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MzMzNTc5NTIsIm5iZiI6MTczMzM1NzY1MiwicGF0aCI6Ii8xNDkzODc1NzgvMzkyNjAyNTkwLTQxOWEyNjhhLWUwMjQtNDVkOC1iMDk1LTEyODI0NDE4MGIzOS5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjQxMjA1JTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI0MTIwNVQwMDE0MTJaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT0yYjNkOWRmOWU2ZWYyNTVmOGRhNDg5ZWQxY2E0ODBmZGI3NGY3YmVkNmRlMGZhYjNjYjY4ZTYxYjNlYTY2MjE4JlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.PZmXMmk0DuByezQsLSWBtNG4DCW0EntabniQdJXLXok">

#### 엔딩 시퀀스 비디오
[Click to Watch](https://www.youtube.com/watch?v=9vdp6t62SyQ)
<a href = "https://www.youtube.com/watch?v=9vdp6t62SyQ" target ="_black">
<image src = "https://private-user-images.githubusercontent.com/149387578/392607552-0cf24456-da35-4836-ac4c-7f06f8596729.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3MzMzNTgxMjYsIm5iZiI6MTczMzM1NzgyNiwicGF0aCI6Ii8xNDkzODc1NzgvMzkyNjA3NTUyLTBjZjI0NDU2LWRhMzUtNDgzNi1hYzRjLTdmMDZmODU5NjcyOS5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjQxMjA1JTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI0MTIwNVQwMDE3MDZaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT04MDMxYjExMjU5OTFmMDA2YzcxNGNiZGY4YjVhMmY5NWU2ZmIwZGNmYTZkY2ZjZmFhMTE2MmQ2YmUxMmU3NTQ3JlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.3udItq6ThZ8TJhRrCarBKAvw-5JvNo_XDhK2IjIqJ3o">
</image></a>
