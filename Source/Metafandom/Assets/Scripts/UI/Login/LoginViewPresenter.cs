using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UniRx.TMPro;
using UnityEngine.SceneManagement;
public class LoginViewPresenter : Presenter
{
    private LoginView _LoginView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    public override void OnInitialize(View view)
    {
        _LoginView = view as LoginView;
        Model.LoginViewModel.InitText();
        InitializeRx();
    }

    /// <summary>
    /// ViewController가 파괴될 때 호출됩니다. 자원 정리 용도로 사용합니다.
    /// </summary>
    public override void OnRelease()
    {
        _LoginView = default;
        _compositeDisposable.Dispose();
    }

    /// <summary>
    /// View에 유저 이벤트가 발생했을 때 동작합니다.
    /// 보통 Model을 업데이트합니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _LoginView.Id.OnValueChangedAsObservable().Subscribe(ChangeEmail).AddTo(_compositeDisposable);
        _LoginView.Password.OnValueChangedAsObservable().Subscribe(ChangePassword).AddTo(_compositeDisposable);


        _LoginView.LoginButton.OnClickAsObservable().Subscribe(_ => LoginRequest()).AddTo(_compositeDisposable);     // 로그인 버튼 클릭
        _LoginView.LoginChoiceButton.OnClickAsObservable().Subscribe(_ => MovePage((int)IntroManager.PageName.IntroPage)).AddTo(_compositeDisposable);  // 로그인 선택 버튼 클릭
        _LoginView.RegisterButton.OnClickAsObservable().Subscribe(_ => MovePage((int)IntroManager.PageName.RegisterPage)).AddTo(_compositeDisposable);     // 회원가입 버튼 클릭
    }


    /// <summary>
    /// Model이 업데이트 되었을 때 동작합니다.
    /// 보통 View를 업데이트합니다.
    /// </summary>
    protected override void OnUpdatedModel()
    {
        Model.LoginViewModel.Email.Subscribe(ApplyEmail).AddTo(_compositeDisposable);
        Model.LoginViewModel.Password.Subscribe(ApplyPassword).AddTo(_compositeDisposable);
    }

    // 모델
    private void ChangeEmail(string text)
    {
        Model.LoginViewModel.SetEmailText(text);
    }
    private void ChangePassword(string text)
    { 
        Model.LoginViewModel.SetPasswordText(text);
    }
    // 뷰
    
    private void ApplyEmail(string text)
    {
        _LoginView.Id.text = text;
    }
    private void ApplyPassword(string text)
    {
        _LoginView.Password.text = text;
    }

    public void LoginRequest()
    {
        // 로그인에 필요한 정보 입력
        var Login_request = new LoginWithEmailAddressRequest { Email = _LoginView.Id.text, Password = _LoginView.Password.text };
        // 값에 따라 로그인 실패 성공 이동 iD값을 여기서 저장
        PlayFabClientAPI.LoginWithEmailAddress(Login_request, OnLoginSuccess, OnLoginFailure);
    }
    // 로그인 성공
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("로그인 성공");
        Model.UserModel.SetId(result.PlayFabId);
        // 다음 페이지로 넘어갈 필요
        MoveScene("Main");
    }
    // 로그인 실패
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("로그인 실패");

        // 메세지 박스 추가 필요
        MessageModel.MessageViewModel.SetMessageBoxText("계정 로그인 오류", "일치하는 회원정보가 없습니다. 아이디 또는 비밀번호를 다시 확인해주세요.", (int)MessageModel.MessageViewModel.MessageBoxStates.LoginFail);
        IntroManager.IntroPage[(int)IntroManager.PageName.MessageBox].SetActive(true);
    }


    // 인트로 내 화면 전환
    private void MovePage(int Page)
    {
        // 페이지 이동방식 작성 필요
        IntroManager.MovePage(Page);
        Model.LoginViewModel.InitText();
    }
    // 씬 이동
    private void MoveScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Model.LoginViewModel.InitText();
    }
}