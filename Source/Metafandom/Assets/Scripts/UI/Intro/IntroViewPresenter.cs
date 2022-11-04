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
    /// ViewController가 파괴될 때 호출됩니다. 자원 정리 용도로 사용합니다.
    /// </summary>
    public override void OnRelease()
    {
        _IntroView = default;
        _compositeDisposable.Dispose();
    }

    /// <summary>
    /// View에 유저 이벤트가 발생했을 때 동작합니다.
    /// 보통 Model을 업데이트합니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _IntroView.LoginButton.OnClickAsObservable().Subscribe(_ => MovePage((int)IntroManager.PageName.LoginPage)).AddTo(_compositeDisposable);        // 로그인 버튼 클릭
        _IntroView.RegisterButton.OnClickAsObservable().Subscribe(_ => MovePage((int)IntroManager.PageName.RegisterPage)).AddTo(_compositeDisposable);     // 회원가입 버튼 클릭
    }

    /// <summary>
    /// Model이 업데이트 되었을 때 동작합니다.
    /// 보통 View를 업데이트합니다.
    /// </summary>
    protected override void OnUpdatedModel()
    {

    }
    private void MovePage(int Page)
    {
        // 페이지 이동방식 작성 필요
        IntroManager.MovePage(Page);
    }
}
