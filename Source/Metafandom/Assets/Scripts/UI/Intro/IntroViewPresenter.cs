using System.Collections;
using System.Collections.Generic;
using UniRx.TMPro;
using UniRx;
using UnityEngine;

public class IntroViewPresenter : Presenter
{
    private IntroView _IntroView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();


    public override void OnInitialize(View view)
    {
        _IntroView = view as IntroView;

        InitializeRx();
    }

    /// <summary>
    /// ViewController�� �ı��� �� ȣ��˴ϴ�. �ڿ� ���� �뵵�� ����մϴ�.
    /// </summary>
    public override void OnRelease()
    {
        _IntroView = default;
        _compositeDisposable.Dispose();
    }

    /// <summary>
    /// View�� ���� �̺�Ʈ�� �߻����� �� �����մϴ�.
    /// ���� Model�� ������Ʈ�մϴ�.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _IntroView.LoginButton.OnClickAsObservable().Subscribe(_ => MovePage((int)IntroManager.PageName.LoginPage)).AddTo(_compositeDisposable);        // �α��� ��ư Ŭ��
        _IntroView.RegisterButton.OnClickAsObservable().Subscribe(_ => MovePage((int)IntroManager.PageName.RegisterPage)).AddTo(_compositeDisposable);     // ȸ������ ��ư Ŭ��
    }

    /// <summary>
    /// Model�� ������Ʈ �Ǿ��� �� �����մϴ�.
    /// ���� View�� ������Ʈ�մϴ�.
    /// </summary>
    protected override void OnUpdatedModel()
    {

    }
    private void MovePage(int Page)
    {
        // ������ �̵���� �ۼ� �ʿ�
        IntroManager.MovePage(Page);
    }
}
