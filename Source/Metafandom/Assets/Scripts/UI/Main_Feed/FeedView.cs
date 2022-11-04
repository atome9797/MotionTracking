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
/// UGUI�� ������ Ŭ�����Դϴ�.
/// </summary>
public class FeedView : View
{
    //������ �迭 ����
    public RectTransform[] Page = new RectTransform[3];

    //�̵�� �÷��̾� �迭 ����
    public MediaPlayer[] MediaPlayers = new MediaPlayer[3];

    //��޿� �÷��̾� �迭 ����
    public VimeoPlayer[] VimeoPlayers = new VimeoPlayer[3];

    //������ �ǵ� ��� ����
    public Image FeedBar { get; private set; }

    //������ �ǵ� ��ư ����
    public Button LastFeedBtn { get; private set; }


    private void Awake()
    {

        for (int i = 0; i < 3; i++)
        {

            //������ �迭 ã��
            int Num = i + 1;
            string PageNum = "Page" + Num;
            Page[i] = transform.Find(PageNum).GetComponent<RectTransform>();
            Debug.Assert(Page[i] != null);

            //�̵�� �÷��̾� �迭 ã��
            string MediaNum = "MediaPlayer" + Num;
            MediaPlayers[i] = transform.Find(MediaNum).GetComponent<MediaPlayer>();
            Debug.Assert(MediaPlayers[i] != null);

            //��޿� �÷��̾� �迭 ã��
            string VimeoNum = "[VimeoPlayer]" + Num;
            VimeoPlayers[i] = transform.Find(VimeoNum).GetComponent<VimeoPlayer>();
            VimeoPlayers[i].autoPlay = false;

            Debug.Assert(VimeoPlayers[i] != null);


        }

        FeedBar = transform.Find("ResetFeedBar").GetComponent<Image>();
        Debug.Assert(FeedBar != null);

        LastFeedBtn = transform.Find("ResetFeedBtn").GetComponent<Button>();
        Debug.Assert(LastFeedBtn != null);

    }

}
