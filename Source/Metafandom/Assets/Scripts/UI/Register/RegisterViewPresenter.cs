using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterViewPresenter : Presenter
{
    RegisterView _RegisterView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public override void OnInitialize(View view)
    {
        _RegisterView = view as RegisterView;
        Model.RegisterViewModel.Init();
        InteractableJoinButton(false);
        InitializeRx();
    }

    public override void OnRelease()
    {
        _RegisterView = default;
        _compositeDisposable.Dispose();
    }

    protected override void OnOccuredUserEvent()
    {
        _RegisterView.Nickname.OnValueChangedAsObservable().Subscribe(ChangeNickName).AddTo(_compositeDisposable);
        _RegisterView.ID.OnValueChangedAsObservable().Subscribe(ChangeID).AddTo(_compositeDisposable);
        _RegisterView.Password.OnValueChangedAsObservable().Subscribe(ChangePassword).AddTo(_compositeDisposable);
        _RegisterView.PasswordCheck.OnValueChangedAsObservable().Subscribe(ChangePasswordCheck).AddTo(_compositeDisposable);


        _RegisterView.JoinButton.OnClickAsObservable().Subscribe(_ => RegisterRequest()).AddTo(_compositeDisposable);     // 회원가입 버튼 클릭

        _RegisterView.BackButton.OnClickAsObservable().Subscribe(_ => RegisterTextMessage()).AddTo(_compositeDisposable);     // 상태 메세지 표시

    }
    protected override void OnUpdatedModel()
    {
        Model.RegisterViewModel.Nickname.Subscribe(ApplyNickname).AddTo(_compositeDisposable);
        Model.RegisterViewModel.ID.Subscribe(ApplyID).AddTo(_compositeDisposable);
        Model.RegisterViewModel.Password.Subscribe(ApplyPassword).AddTo(_compositeDisposable);
        Model.RegisterViewModel.PasswordCheck.Subscribe(ApplyPasswordCheck).AddTo(_compositeDisposable);

        Model.RegisterViewModel.IDStatus.Subscribe(ApplyIDState).AddTo(_compositeDisposable);
        Model.RegisterViewModel.PasswordStatus.Subscribe(ApplyPasswordState).AddTo(_compositeDisposable);
        Model.RegisterViewModel.PasswordCheckStatus.Subscribe(ApplyPasswordCheckState).AddTo(_compositeDisposable);
    }

    // 모델
    private void ChangeNickName(string text)
    {
        Model.RegisterViewModel.SetNicknameText(text);
    }
    private void ChangeID(string text)
    {
        Model.RegisterViewModel.SetIDText(text);
    }
    private void ChangePassword(string text)
    {
        Model.RegisterViewModel.SetPasswordText(text);
    }       
    private void ChangePasswordCheck(string text)
    {
        Model.RegisterViewModel.SetPasswordCheckText(text);
    }

    // 뷰
    private void ApplyNickname(string text)
    {
        _RegisterView.Nickname.text = text;
    }
    private void ApplyID(string text)
    {
        _RegisterView.ID.text = text;
    }
    private void ApplyPassword(string text)
    {
        _RegisterView.Password.text = text;
    }
    private void ApplyPasswordCheck(string text)
    {
        _RegisterView.PasswordCheck.text = text;
    }
    // 아이디, 비밀번호, 비밀번호 확인 상태에 따라 메세지 바꾸기
    private void ApplyIDState(int Num)
    {
        _RegisterView.SetIdGuide(Num);
        InteractableJoinButton(RegisterCheck());
    }
    private void ApplyPasswordState(int Num)
    {
        _RegisterView.SetPasswordGuide(Num);
        InteractableJoinButton(RegisterCheck());
    }
    private void ApplyPasswordCheckState(int Num)
    {
        _RegisterView.SetPasswordCheckGuide(Num);
        InteractableJoinButton(RegisterCheck());
    }

    //상태 메세지 호출
    private void RegisterTextMessage()
    {
        MessageModel.MessageViewModel.SetMessageBoxText("회원가입 중단 알림", "회원가입 절차를 중단하시겠습니까?", (int)MessageModel.MessageViewModel.MessageBoxStates.RegisterFail);
        IntroManager.IntroPage[(int)IntroManager.PageName.MessageBox].SetActive(true);
    }

    // 회원가입
    private void RegisterRequest()
    {
        // 회원가입에 필요한 정보 입력
        var Email_request = new RegisterPlayFabUserRequest { Email = Model.RegisterViewModel.ID.Value, Password = Model.RegisterViewModel.Password.Value, Username = Model.RegisterViewModel.Nickname.Value };
        
        // 회원가입
        PlayFabClientAPI.RegisterPlayFabUser(Email_request, OnRegisterSuccess, OnRegisterFailire);
        
    }

    /// <summary>
    /// 회원가입 조건 만족했는지 체크
    /// </summary>
    /// <returns></returns>
    private bool RegisterCheck()
    {
        if (Model.RegisterViewModel.IDStatus.Value == (int)Model.RegisterViewModel.IdStatusEnum.Enable && Model.RegisterViewModel.PasswordStatus.Value == (int)Model.RegisterViewModel.PasswordStatusEnum.PasswordMatch && Model.RegisterViewModel.PasswordCheckStatus.Value == (int)Model.RegisterViewModel.PasswordCheckStatusEnum.Enable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 조인 버튼 활성화 비활성화
    /// </summary>
    /// <param name="Value"></param>
    private void InteractableJoinButton(bool Value)
    {
        _RegisterView.JoinButton.interactable = Value;
    }

    // 회원가입 성공
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
        MovePage((int)IntroManager.PageName.RegisterSuccessPage);
    }
    // 회원가입 실패
    private void OnRegisterFailire(PlayFabError error)
    {
        Debug.LogWarning("회원가입 실패");

        // 메세지 박스 추가 필요
    }

    //페이지 이동
    private void MovePage(int Page)
    {
        Model.RegisterViewModel.Init();
        InteractableJoinButton(false);
        IntroManager.MovePage(Page);
    }
}
