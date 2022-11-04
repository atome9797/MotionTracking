using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CommonParenter : Presenter
{
    private CommonView _commonView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    public override void OnInitialize(View view)
    {
        _commonView = view as CommonView;

        InitializeRx();
    }

    public override void OnRelease()
    {
        _commonView = default;
        _compositeDisposable.Dispose();
    }

    protected override void OnOccuredUserEvent()
    {
        _commonView.CallButton.OnClickAsObservable().Subscribe(loadScene).AddTo(_compositeDisposable);



    }

    protected override void OnUpdatedModel()
    {
        Model.CommonViewModel.ISUploadSceneActive.Subscribe(MenuBarChangeState).AddTo(_compositeDisposable);

        Model.UploadSeneModel.uploadState.Subscribe(showMessageBox).AddTo(_compositeDisposable);
        Model.UploadSeneModel.uploadState.Subscribe(ChangeMessageText).AddTo(_compositeDisposable);
    }

    private void loadScene(Unit unit)
    {
        MainSceneManager.Instance.changeScene("Main_Upload");
        Model.CommonViewModel.setUploadSceneState(true);
    }

    private void MenuBarChangeState(bool state)
    {
        _commonView.MenuBar.SetActive(!state);
    }

    private void showMessageBox(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            _commonView.MessageBox.SetActive(false);
        }
        else
            _commonView.MessageBox.SetActive(true);
    }

    private void ChangeMessageText(string text)
    {
        if (text == "UploadComplete")
            _commonView.MessageText.text = "게시물 업로드 완료";
        else 
            _commonView.MessageText.text = "게시물 업로드 중";
    }

}
