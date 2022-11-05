using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using RenderHeads.Media.AVProVideo;
using RenderHeads.Media.AVProVideo.Demos.UI;
using Vimeo.Player;

/// <summary>
/// UGUI를 참조할 클래스입니다.
/// </summary>
public class FeedView : View
{
    //페이지 배열 생성
    public RectTransform[] Page = new RectTransform[3];

    //미디어 플레이어 배열 생성
    public MediaPlayer[] MediaPlayers = new MediaPlayer[3];

    //비메오 플레이어 배열 생성
    public VimeoPlayer[] VimeoPlayers = new VimeoPlayer[3];

    //마지막 피드 배너 생성
    public Image FeedBar { get; private set; }

    //마지막 피드 버튼 생성
    public Button LastFeedBtn { get; private set; }

    //좋아요 버튼 생성
    public Button LikeFeedBtn { get; private set; }
    //북마크 버튼 생성
    public Button BookmarkBtn { get; private set; }
    //공유하기 버튼 생성
    public Button ShareBtn { get; private set; }
    //더보기 버튼 생성
    public Button MoreViewBtn { get; private set; }
    //닉네임 텍스트 생성
    public TextMeshProUGUI NickName { get; private set; }
    //피드 내용 생성
    public TextMeshProUGUI FeedContent { get; private set; }
    //피드 지역 내용 생성
    public TextMeshProUGUI FeedLocation { get; private set; }



    private void Awake()
    {

        for (int i = 0; i < 3; i++)
        {

            //페이지 배열 찾기
            int Num = i + 1;
            string PageNum = "Page" + Num;
            Page[i] = transform.Find(PageNum).GetComponent<RectTransform>();
            Debug.Assert(Page[i] != null);

            //미디어 플레이어 배열 찾기
            string MediaNum = "MediaPlayer" + Num;
            MediaPlayers[i] = transform.Find(MediaNum).GetComponent<MediaPlayer>();
            Debug.Assert(MediaPlayers[i] != null);

            //비메오 플레이어 배열 찾기
            string VimeoNum = "[VimeoPlayer]" + Num;
            VimeoPlayers[i] = transform.Find(VimeoNum).GetComponent<VimeoPlayer>();
            VimeoPlayers[i].autoPlay = false;

            Debug.Assert(VimeoPlayers[i] != null);


        }

        //피드 초기화 배너
        FeedBar = transform.Find("ResetFeedBar").GetComponent<Image>();
        Debug.Assert(FeedBar != null);

        //피드 초기화 버튼
        LastFeedBtn = transform.Find("ResetFeedBtn").GetComponent<Button>();
        Debug.Assert(LastFeedBtn != null);

        //좋아요 버튼
        LikeFeedBtn = transform.Find("LikeFeedBtn").GetComponent<Button>();
        Debug.Assert(LikeFeedBtn != null);

        //북마크 버튼
        BookmarkBtn = transform.Find("BookmarkBtn").GetComponent<Button>();
        Debug.Assert(BookmarkBtn != null);
        //공유하기 버튼
        ShareBtn = transform.Find("ShareBtn").GetComponent<Button>();
        Debug.Assert(ShareBtn != null);
        //더보기 버튼
        MoreViewBtn = transform.Find("MoreViewBtn").GetComponent<Button>();
        Debug.Assert(MoreViewBtn != null);
        //닉네임 텍스트
        NickName = transform.Find("NickName").GetComponent<TextMeshProUGUI>();
        Debug.Assert(NickName != null);
        //피드 내용
        FeedContent = transform.Find("FeedContent").GetComponent<TextMeshProUGUI>();
        Debug.Assert(FeedContent != null);
        //피드 지역 내용
        FeedLocation = transform.Find("FeedLocation").GetComponent<TextMeshProUGUI>();
        Debug.Assert(FeedLocation != null);

    }

}
