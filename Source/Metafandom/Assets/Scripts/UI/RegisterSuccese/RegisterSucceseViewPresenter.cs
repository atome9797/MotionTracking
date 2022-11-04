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
    /// ViewController가 파괴될 때 호출됩니다. 자원 정리 용도로 사용합니다.
    /// </summary>
    public override void OnRelease()
    {
        _RegisterSucceseView = default;
        _compositeDisposable.Dispose();
    }

    /// <summary>
    /// View에 유저 이벤트가 발생했을 때 동작합니다.
    /// 보통 Model을 업데이트합니다.
    /// </summary>
    protected override void OnOccuredUserEvent()
    {
        _RegisterSucceseView.RegisterSucceseButton.OnClickAsObservable().Subscribe(_ => MoveScene("Main")).AddTo(_compositeDisposable);        // 회원가입 완료 버튼 클릭
    }

    /// <summary>
    /// Model이 업데이트 되었을 때 동작합니다.
    /// 보통 View를 업데이트합니다.
    /// </summary>
    protected override void OnUpdatedModel()
    {

    }
    private void MoveScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
