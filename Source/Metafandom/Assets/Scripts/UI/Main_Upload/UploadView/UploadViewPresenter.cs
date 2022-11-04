using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Vimeo.Recorder;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;


public class UploadViewPresenter : Presenter
{
    private UploadView _uploadView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();


    public override void OnInitialize(View view)
    {
        _uploadView = view as UploadView;
        _uploadView.gameObject.SetActive(false);
        Model.UploadSeneModel.initcategory();

        InitializeRx();
    }

    public override void OnRelease()
    {
        _uploadView = default;
        _compositeDisposable.Dispose();
    }

    protected override void OnOccuredUserEvent()
    {
        _uploadView.ExitButton.OnClickAsObservable().Subscribe(deActiveUploadView).AddTo(_compositeDisposable);
        _uploadView.UploadButton.OnClickAsObservable().Subscribe(UploadVideo).AddTo(_compositeDisposable);
        _uploadView.UploadButton.OnClickAsObservable().Subscribe(deActiveUploadView).AddTo(_compositeDisposable);
        _uploadView.BackButton.OnClickAsObservable().Subscribe(LoadScene).AddTo(_compositeDisposable);

        _uploadView.CategoryToggles[0].OnValueChangedAsObservable().Subscribe(x => setCategory(x, _uploadView.CategoryToggles[0].name)).AddTo(_compositeDisposable);
        _uploadView.CategoryToggles[1].OnValueChangedAsObservable().Subscribe(x => setCategory(x, _uploadView.CategoryToggles[1].name)).AddTo(_compositeDisposable);
        _uploadView.CategoryToggles[2].OnValueChangedAsObservable().Subscribe(x => setCategory(x, _uploadView.CategoryToggles[2].name)).AddTo(_compositeDisposable);
    }

    protected override void OnUpdatedModel()
    {
        Model.UploadSeneModel.IsUploadViewActive.Subscribe(UploadViewStateTrans).AddTo(_compositeDisposable);

        Model.UploadSeneModel.uploadState.Subscribe(PlayfabUpload).AddTo(_compositeDisposable);

        Model.UploadSeneModel.videoThumbnail.Subscribe(loadThumbnail).AddTo(_compositeDisposable);        
    }

    private void deActiveUploadView(Unit unit)
    {
        Model.UploadSeneModel.ChangeView(false);
    }

    private void UploadViewStateTrans(bool active)
    {
        _uploadView.gameObject.SetActive(active);

        for (int i = 0; i < _uploadView.CategoryToggles.Count; ++i)
        {
            _uploadView.CategoryToggles[i].isOn = false;
        }
    }

    private void UploadVideo(Unit unit)
    {
        MainSceneManager.Instance._recoder.PublishVideo(Model.UploadSeneModel.IsEncording.Value, Model.UploadSeneModel.Path.Value);
        MainSceneManager.Instance._recoder.publisher.OnUploadProgress += Uploadcheck;
        Model.UploadSeneModel.chageCategory();
        backtoFeed();
    }

    private void Uploadcheck(string state, float progress)
    {
        Model.UploadSeneModel.changeUploadState(state);
    }
    /// <summary>
    /// Vimeo�� ������ �� �ö󰡸� PlayFab�� ���������� �߰��մϴ�.
    /// </summary>
    /// <param name="state">Vimeo ���ε� ��Ȳ</param>
    private void PlayfabUpload(string state)
    {
        if (state != "UploadComplete")
            return;
        Model.UploadSeneModel.LoadVideo(null);
        Model.VideoPost videoPost = new Model.VideoPost();
        videoPost.vimeo_id = MainSceneManager.Instance._recoder.publisher.video.id.ToString();
        videoPost.user_id = Model.UserModel.Id.Value;
        videoPost.video_name = "asd";
        videoPost.index = 0;
        videoPost.video_upload_date = System.DateTime.Now.ToString();
        videoPost.category_id_list = Model.UploadSeneModel.Category;
        InsertPostData(videoPost);
        Model.UploadSeneModel.initcategory();
    }

    private void LoadScene(Unit unit)
    {
        backtoFeed();
    }

    private void backtoFeed()
    {
        MainSceneManager.Instance.changeScene("Main_Feed");
        Model.CommonViewModel.setUploadSceneState(false);
        Model.UploadSeneModel.ChangeView(false);
        Model.UploadSeneModel.LoadVideo(null);
    }

    private void setCategory(bool value, string name)
    {
        Model.UploadSeneModel.setcategory[matchcategory(name)] = value;
        Debug.Log(matchcategory(name));

    }
    private int matchcategory(string name)
    {
        string temp = name.Substring(8);// �տ� Category���� ������ ����
        return Model.CategoryModel.VideoCategoryDataList.IndexOfValue(name.Substring(8));
    }

    private void loadThumbnail(Texture2D thumbnail)
    {
        _uploadView.ThumbnailWindow.texture = thumbnail;
    }

    /// <summary>
    /// ���� ������ ���� �Լ�
    /// </summary>
    /// <param name="PostData"></param>
    public void InsertPostData(Model.VideoPost PostData)
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "InsertPostData",
            FunctionParameter = new
            {
                vimeo_id = PostData.vimeo_id,
                user_id = PostData.user_id,
                user_name = PostData.user_name,
                video_name = PostData.video_name,
                category_id_list = PostData.category_id_list
            }
        };

        PlayFabClientAPI.ExecuteCloudScript(request, OnInsertPostDataSuccess, OnInsertPostDataError);
    }

    /// <summary>
    /// ���� ������ ���� ������ �ݹ��Լ�
    /// </summary>
    /// <param name="result"></param>
    public void OnInsertPostDataSuccess(ExecuteCloudScriptResult result)
    {
        Model.UploadSeneModel.changeUploadState(null);
        Debug.Log("����");
    }

    /// <summary>
    /// ���� ������ ���� ���н� �ݹ��Լ�
    /// </summary>
    /// <param name="error"></param>
    public void OnInsertPostDataError(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }





}
