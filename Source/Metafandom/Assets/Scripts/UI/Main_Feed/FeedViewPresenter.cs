using UniRx;
using UniRx.Triggers;
using UniRx.TMPro;
using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public sealed class FeedViewPresenter : Presenter
{
    private FeedView FeedView;
    private CompositeDisposable CompositeDisposable = new CompositeDisposable();

    List<int> PageNumberIndexList = new List<int>();

    public float SwipeLength { get; set; }

    public Vector2 StartPos { get; set; }
    public Vector2 EndPos { get; set; }

    //������ ��� ����
    public bool IsVideoPlay = true;

    public override void OnInitialize(View view)
    {
        FeedView = view as FeedView;

        InitializeRx();

        //������ �ѹ� �ʱ�ȭ
        InitSetPageNumberList();
    }

    public override void OnRelease()
    {
        FeedView = default;
        CompositeDisposable.Dispose();
    }

    protected override void OnOccuredUserEvent()
    {
        //터치 한번 눌렀을때 시작 위치 지정
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => StartPos = Input.mousePosition);

        //터치 땠을때 스와이프 기능 구현
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => MouseButtonUp());

        //터치중에 화면 움직임 조절
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ => TouchMouseEvent());

        //영상 정지 플레이 기능 (가운데 영상만 재생 되도록 설정)
        Observable.EveryUpdate()
            .Subscribe(_ => FeedView.MediaPlayers[PageNumberIndexList[0]].Pause());
        Observable.EveryUpdate()
            .Subscribe(_ => FeedView.MediaPlayers[PageNumberIndexList[2]].Pause());
        Observable.EveryUpdate()
            .Where(_ => IsVideoPlay)
            .Subscribe(_ => FeedView.MediaPlayers[PageNumberIndexList[1]].Play());

        //VideoInitSuccess 값이 true일때 실행
        Model.FeedViewModel.VideoInitSuccess.Subscribe(VideoInitSuccess).AddTo(CompositeDisposable);


        //마지막 피드일때 초기화
        FeedView.LastFeedBtn.onClick
            .AsObservable()
            .Subscribe(_ => ResetVideoList());

    }


    private void VideoInitSuccess(bool check)
    {
        if (check)
        {
            InitLoadVideo();
        }
    }


    private void ResetVideoList()
    {
        Model.FeedViewModel.VideoIndex = 0;
        Model.FeedViewModel.VideoTotalIndex = 0;
        Model.FeedViewModel.VideoMultipleIndex = 1;
        Model.FeedViewModel.InitializePostData(11, "2100000000");

        //�� �Ʒ� �ƴϸ� ��Ȱ��ȭ
        FeedView.LastFeedBtn.gameObject.SetActive(false);
        FeedView.FeedBar.gameObject.SetActive(false);
    }

    protected override void OnUpdatedModel()
    {
        //���� ���� �������� 11��
        Model.FeedViewModel.InitializePostData(11, "2100000000");
    }

    private void InitLoadVideo()
    {
        FeedView.VimeoPlayers[PageNumberIndexList[0]].autoPlay = true;
        FeedView.VimeoPlayers[PageNumberIndexList[1]].autoPlay = true;
        FeedView.VimeoPlayers[PageNumberIndexList[2]].autoPlay = true;

        FeedView.VimeoPlayers[PageNumberIndexList[1]].LoadVideo(Model.FeedViewModel.PostData[0].vimeo_id);
        FeedView.VimeoPlayers[PageNumberIndexList[2]].LoadVideo(Model.FeedViewModel.PostData[1].vimeo_id);
    }


    private void TouchResetEvent()
    {
        FeedView.Page[PageNumberIndexList[0]].DOAnchorPos3D(new Vector3(0, 780, 300), 0.25f);

        //�߽�
        FeedView.Page[PageNumberIndexList[1]].DOAnchorPos3D(new Vector3(0, 0, 0), 0.25f);

        //������
        FeedView.Page[PageNumberIndexList[2]].DOAnchorPos3D(new Vector3(0, -850, 300), 0.25f);
    }

    private void TouchMouseEvent()
    {
        EndPos = Input.mousePosition;
        SwipeLength = EndPos.y - StartPos.y;

        SwipeLength *= 0.7f;


        if (SwipeLength < 0)
        {
            //�� ó�� �϶��� �ȿö󰡰� ����
            if (Model.FeedViewModel.VideoIndex > 0)
            {
                FeedView.Page[PageNumberIndexList[1]].DOAnchorPos(new Vector2(0, SwipeLength), 0.25f);
                FeedView.Page[PageNumberIndexList[0]].DOAnchorPos(new Vector2(0, SwipeLength + 780f), 0.25f);
                FeedView.Page[PageNumberIndexList[2]].DOAnchorPos(new Vector2(0, SwipeLength - 850f), 0.25f);

                //맨위로 올라가면 가려지게됨
                FeedView.Page[PageNumberIndexList[2]].transform.SetSiblingIndex(1);
            }
        }
        else
        {
            //���� �ε����� Max �� ��� �� �ǵ� ������ �ϱ�
            if (Model.FeedViewModel.VideoIndex < Model.FeedViewModel.VideoTotalIndex)
            {
                FeedView.Page[PageNumberIndexList[1]].DOAnchorPos(new Vector2(0, SwipeLength), 0.25f);
                FeedView.Page[PageNumberIndexList[0]].DOAnchorPos(new Vector2(0, SwipeLength + 780f), 0.25f);
                FeedView.Page[PageNumberIndexList[2]].DOAnchorPos(new Vector2(0, SwipeLength - 850f), 0.25f);

                //맨 위로 올라가면 가려지게됨
                FeedView.Page[PageNumberIndexList[0]].transform.SetSiblingIndex(1);
            }
        }
    }


    private void MouseButtonUp()
    {
        EndPos = Input.mousePosition;
        SwipeLength = EndPos.y - StartPos.y;

        if (SwipeLength >= 80)
        {
            NextPage();
            SwipeLength = 0f;
        }
        else if (SwipeLength <= -80)
        {
            PrevPage();
            SwipeLength = 0f;
        }
        else if (SwipeLength == 0)
        {
            IsVideoPlay = !IsVideoPlay;
            if (!IsVideoPlay)
            {
                FeedView.MediaPlayers[PageNumberIndexList[1]].Pause();
            }
        }

        TouchResetEvent();

    }

    private void InitSetPageNumberList()
    {
        for (int i = 0; i < 3; i++)
        {
            PageNumberIndexList.Add(i);
        }
    }

    private void NextPage()
    {

        //���� �ε����� Max �� ��� �� �ǵ� ������ �ϱ�
        if (Model.FeedViewModel.VideoIndex < Model.FeedViewModel.VideoTotalIndex)
        {
            int frontNumber = PageNumberIndexList[0];
            PageNumberIndexList.RemoveAt(0);
            PageNumberIndexList.Add(frontNumber);

            MovePage();
            Model.FeedViewModel.VideoIndex += 1;


            //���� ���ο� ���� �ε�
            if (Model.FeedViewModel.VideoIndex < Model.FeedViewModel.VideoTotalIndex)
            {
                //���� �������� ���ε�
                FeedView.VimeoPlayers[PageNumberIndexList[2]].LoadVideo(Model.FeedViewModel.PostData[Model.FeedViewModel.VideoIndex + 1].vimeo_id);
                FeedView.MediaPlayers[PageNumberIndexList[0]].OpenMedia();

                //�ε����� 5�� ����̰� ���� �����͸� �߰��������� +1����
                if (Model.FeedViewModel.VideoIndex % (5 * Model.FeedViewModel.VideoMultipleIndex) == 0)
                {
                    Model.FeedViewModel.NextPostData(11, Model.FeedViewModel.PostData[Model.FeedViewModel.VideoTotalIndex].vimeo_id);
                }
            }


        }
        else
        {
            //�� ���̸� ȭ���̸� ȭ�� ������ ���� �� 
            FeedView.LastFeedBtn.gameObject.SetActive(true);
            FeedView.FeedBar.gameObject.SetActive(true);
        }


    }

    private void PrevPage()
    {
        //���� �ε����� 0�� ��� �� �ǵ� ������ �ϱ�
        if (Model.FeedViewModel.VideoIndex > 0)
        {
            int backNumber = PageNumberIndexList[PageNumberIndexList.Count - 1];
            PageNumberIndexList.RemoveAt(PageNumberIndexList.Count - 1);
            PageNumberIndexList.Insert(0, backNumber);


            MovePage();
            Model.FeedViewModel.VideoIndex -= 1;
            //���� ���ο� ���� �ε�
            if (Model.FeedViewModel.VideoIndex - 1 >= 0)
            {
                FeedView.VimeoPlayers[PageNumberIndexList[0]].LoadVideo(Model.FeedViewModel.PostData[Model.FeedViewModel.VideoIndex - 1].vimeo_id);
                FeedView.MediaPlayers[PageNumberIndexList[2]].OpenMedia();
            }
        }

        //-1�� ��� �������� ����

        //�� �Ʒ� �ƴϸ� ��Ȱ��ȭ
        FeedView.LastFeedBtn.gameObject.SetActive(false);
        FeedView.FeedBar.gameObject.SetActive(false);

    }

    private void MovePage()
    {

        //����
        FeedView.Page[PageNumberIndexList[0]].DOAnchorPos3D(new Vector3(0, 780, 300), 0.25f);

        //�߽�
        FeedView.Page[PageNumberIndexList[1]].DOAnchorPos3D(new Vector3(0, 0, 0), 0.25f);

        //������
        FeedView.Page[PageNumberIndexList[2]].DOAnchorPos3D(new Vector3(0, -850, 300), 0.25f);

        IsVideoPlay = true;
    }


}