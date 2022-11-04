using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.IO;

public class LoadViewPresenter : Presenter
{
    private LoadView _loadView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();


    public override void OnInitialize(View view)
    {
        _loadView = view as LoadView;
        _loadView.LoadingErrorText.SetActive(false);

        InitializeRx();
    }

    public override void OnRelease()
    {
        _loadView = default;
        _compositeDisposable.Dispose();
    }

    protected override void OnOccuredUserEvent()
    {
        _loadView.LoadVideo.OnClickAsObservable().Subscribe(ImportVideoPath).AddTo(_compositeDisposable);


        _loadView.NextButton.OnClickAsObservable().Subscribe(activeUploadScene).AddTo(_compositeDisposable);

        _loadView.OnEnableAsObservable().Subscribe(PlayVideo).AddTo(_compositeDisposable);

        _loadView.BackButton.OnClickAsObservable().Subscribe(LoadScene).AddTo(_compositeDisposable);
    }


    protected override void OnUpdatedModel()
    {
        Model.UploadSeneModel.Path.Subscribe(PlayVideo).AddTo(_compositeDisposable);

        Model.UploadSeneModel.IsUploadViewActive.Subscribe(loadViewStateTrans).AddTo(_compositeDisposable);

    }

    /// <summary>
    /// NativeGallery�� ȣ���Ͽ� ������ ������ ��θ� �����ɴϴ�.
    /// </summary>
    /// <param name="unit"></param>
    private void ImportVideoPath(Unit unit)
    {
        NativeGallery.GetVideoFromGallery((file) =>
        {
            //FileInfo selected = new FileInfo(file);
            //Debug.Assert(selected != null);
            //// �뷮 ����
            //if (selected.Length > 50000000)
            //{
            //    return;
            //}

            // �ҷ�����
            if (!string.IsNullOrEmpty(file))
            {
                Model.UploadSeneModel.LoadVideo(file);
                Model.UploadSeneModel.SetThumbnail(NativeGallery.GetVideoThumbnail(file, captureTimeInSeconds: 0.0));
            }

        });
    }

    /// <summary>
    /// Avpro�� ������ ����մϴ�.
    /// </summary>
    /// <param name="path">�����ų ������ ���</param>
    private void PlayVideo(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        _loadView.mediaPlayer.OpenMedia(RenderHeads.Media.AVProVideo.MediaPathType.AbsolutePathOrURL, path);
    }

    private void PlayVideo(Unit unit)
    {
        if (string.IsNullOrEmpty(Model.UploadSeneModel.Path.Value))
        {
            _loadView.mediaPlayer.CloseMedia();
            return;
        }
        _loadView.mediaPlayer.OpenMedia(RenderHeads.Media.AVProVideo.MediaPathType.AbsolutePathOrURL, Model.UploadSeneModel.Path.Value);
    }



    private void activeUploadScene(Unit unit)
    {
        if (string.IsNullOrEmpty(Model.UploadSeneModel.Path.Value))
        {
            _loadView.ShowErrorText();
            return;
        }
        Model.UploadSeneModel.ChangeView(true);
        Model.UploadSeneModel.initcategory();
        _loadView.gameObject.SetActive(false);
    }

    private void loadViewStateTrans(bool active)
    {
        _loadView.gameObject.SetActive(!active);
    }

    private void LoadScene(Unit unit)
    {
        MainSceneManager.Instance.changeScene("Main_Feed");
        Model.CommonViewModel.setUploadSceneState(false);
        Model.UploadSeneModel.LoadVideo(null);
    }

}