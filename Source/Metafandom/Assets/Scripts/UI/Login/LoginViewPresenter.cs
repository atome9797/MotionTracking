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
    /// ViewController�� �ı��� �� ȣ��˴ϴ�. �ڿ� ���� �뵵�� ����մϴ�.
    /// </summary>
    public override void OnRelease()
    {
        _LoginView = default;
        _compositeDisposable.Dispose();
    }

    /// <summary>
    /// View�� ���� �̺�Ʈ�� �߻����� �� �����մϴ�.
    /// ���� Model�� ������Ʈ�մϴ�.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _LoginView.Id.OnValueChangedAsObservable().Subscribe(ChangeEmail).AddTo(_compositeDisposable);
        _LoginView.Password.OnValueChangedAsObservable().Subscribe(ChangePassword).AddTo(_compositeDisposable);


        _LoginView.LoginButton.OnClickAsObservable().Subscribe(_ => LoginRequest()).AddTo(_compositeDisposable);     // �α��� ��ư Ŭ��
        _LoginView.LoginChoiceButton.OnClickAsObservable().Subscribe(_ => MovePage((int)IntroManager.PageName.IntroPage)).AddTo(_compositeDisposable);  // �α��� ���� ��ư Ŭ��
        _LoginView.RegisterButton.OnClickAsObservable().Subscribe(_ => MovePage((int)IntroManager.PageName.RegisterPage)).AddTo(_compositeDisposable);     // ȸ������ ��ư Ŭ��
    }


    /// <summary>
    /// Model�� ������Ʈ �Ǿ��� �� �����մϴ�.
    /// ���� View�� ������Ʈ�մϴ�.
    /// </summary>
    protected override void OnUpdatedModel()
    {
        Model.LoginViewModel.Email.Subscribe(ApplyEmail).AddTo(_compositeDisposable);
        Model.LoginViewModel.Password.Subscribe(ApplyPassword).AddTo(_compositeDisposable);
    }

    // ��
    private void ChangeEmail(string text)
    {
        Model.LoginViewModel.SetEmailText(text);
    }
    private void ChangePassword(string text)
    { 
        Model.LoginViewModel.SetPasswordText(text);
    }
    // ��
    
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
        // �α��ο� �ʿ��� ���� �Է�
        var Login_request = new LoginWithEmailAddressRequest { Email = _LoginView.Id.text, Password = _LoginView.Password.text };
        // ���� ���� �α��� ���� ���� �̵� iD���� ���⼭ ����
        PlayFabClientAPI.LoginWithEmailAddress(Login_request, OnLoginSuccess, OnLoginFailure);
    }
    // �α��� ����
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("�α��� ����");
        Model.UserModel.SetId(result.PlayFabId);
        // ���� �������� �Ѿ �ʿ�
        MoveScene("Main");
    }
    // �α��� ����
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("�α��� ����");

        // �޼��� �ڽ� �߰� �ʿ�
        MessageModel.MessageViewModel.SetMessageBoxText("���� �α��� ����", "��ġ�ϴ� ȸ�������� �����ϴ�. ���̵� �Ǵ� ��й�ȣ�� �ٽ� Ȯ�����ּ���.", (int)MessageModel.MessageViewModel.MessageBoxStates.LoginFail);
        IntroManager.IntroPage[(int)IntroManager.PageName.MessageBox].SetActive(true);
    }


    // ��Ʈ�� �� ȭ�� ��ȯ
    private void MovePage(int Page)
    {
        // ������ �̵���� �ۼ� �ʿ�
        IntroManager.MovePage(Page);
        Model.LoginViewModel.InitText();
    }
    // �� �̵�
    private void MoveScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Model.LoginViewModel.InitText();
    }
}