using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadView : View
{
    public Button BackButton { get; private set; }
    public Button LoadVideo { get; private set; }
    public Button NextButton { get; private set; }

    public GameObject LoadingErrorText { get; private set; }

    public RenderHeads.Media.AVProVideo.MediaPlayer mediaPlayer { get; private set; }

    private void Awake()
    {
        BackButton = transform.Find("BackButton").GetComponent<Button>();
        Debug.Assert(BackButton != null);

        NextButton = transform.Find("NextButton").GetComponent<Button>();
        Debug.Assert(BackButton != null);

        LoadVideo = transform.Find("VideoLoadButton").GetComponent<Button>();
        Debug.Assert(BackButton != null);

        LoadingErrorText = transform.Find("LoadingErrorText").gameObject;
        Debug.Assert(BackButton != null);

        mediaPlayer = transform.Find("MediaPlayer").GetComponent<RenderHeads.Media.AVProVideo.MediaPlayer>();
        Debug.Assert(mediaPlayer != null);
    }

    public void ShowErrorText()
    {
        if (!LoadingErrorText.activeSelf)
            Invoke("HideErrorText", 2f);
        LoadingErrorText.SetActive(true);
    }

    public void HideErrorText()
    {
        LoadingErrorText.SetActive(false);
    }
}
