---
name: UI 레이아웃 구성
about: 화면 UI 레이아웃 구성을 요청합니다.
title: "[Feature]"
labels: enhancement
assignees: ''

---

# 설명
{화면_이름} 화면의 UI 레이아웃을 구성합니다.

UI 하이어라키는 아래와 같이 구성합니다.

```
ViewController (Canvas)             # 캔버스
ㄴ View (Empty GameObject)          # 뷰 
    ㄴ Name (Type)                  # 어떤 요소
```

# 참고자료
- [UI 디자인 문서](https://xd.adobe.com/view/67f8aab8-1eee-4abd-adce-d7686fc4c376-32ab/)

# 주의사항
- ViewController의 Canvas 컴포넌트 속성은 아래와 같아야 합니다.
  - **Reference Pixels Per Unit** 을 3으로 지정해주세요.
  - UI Scale Mode를 **Screen With Scale Size** 로 지정해주세요.
  - **Reference Resolution** 을 375 x 812 로 해주세요.
![ViewController](https://user-images.githubusercontent.com/17216686/197437867-27cf8786-ff9b-4436-8e81-4d080677e412.png)

- View의 RectTransform 컴포넌트 속성은 아래와 같아야 합니다.
    - 앵커 프리셋을 아래처럼 지정해주세요.
    - **View를 제외한 UI 요소는 배치되는 곳에 따라 각각 다른 앵커 프리셋을 사용해야 합니다!**
![View](https://user-images.githubusercontent.com/17216686/197428741-4c25d8ea-1d43-4e5d-83aa-5c239f6ce8c2.png)