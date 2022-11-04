using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterSucceseViewPresenter : Presenter
{
    private RegisterSucceseView _RegisterSucceseView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public override void OnInitialize(View view)
    {
        _RegisterSucceseView = view as RegisterSucceseView;

        InitializeRx();
    }

    /// <summary>
    /// ViewController�� �ı��� �� ȣ��˴ϴ�. �ڿ� ���� �뵵�� ����մϴ�.
    /// </summary>
    public override void OnRelease()
    {
        _RegisterSucceseView = default;
        _compositeDisposable.Dispose();
    }

    /// <summary>
    /// View�� ���� �̺�Ʈ�� �߻����� �� �����մϴ�.
    /// ���� Model�� ������Ʈ�մϴ�.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _RegisterSucceseView.RegisterSucceseButton.OnClickAsObservable().Subscribe(_ => MoveScene("Main")).AddTo(_compositeDisposable);        // ȸ������ �Ϸ� ��ư Ŭ��
    }

    /// <summary>
    /// Model�� ������Ʈ �Ǿ��� �� �����մϴ�.
    /// ���� View�� ������Ʈ�մϴ�.
    /// </summary>
    protected override void OnUpdatedModel()
    {

    }
    private void MoveScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
