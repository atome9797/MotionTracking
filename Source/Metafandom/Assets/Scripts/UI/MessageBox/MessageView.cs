using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum UINT
{
    Ok,
    OkCancel
}

public class MessageView : View
{
    public TextMeshProUGUI TitleText { get; private set; }
    public TextMeshProUGUI Message { get; private set; }
    public GameObject Footer { get; private set; }
    public GameObject SingleChooseButton { get; private set; }
    public GameObject MultipleChooseButton { get; private set; }
    public Button SingleOkButton { get; private set; }
    public Button MultipleOkButton { get; private set; }
    public Button MultipleCancelButton { get; private set; }

    private void Awake()
    {
        TitleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        Debug.Assert(TitleText != null);

        Message = transform.Find("Message").GetComponent<TextMeshProUGUI>();
        Debug.Assert(Message != null);
        
        Footer = transform.Find("Footer").gameObject;
        Debug.Assert(Footer != null);

        SingleChooseButton = Footer.transform.Find("Ok").gameObject;
        Debug.Assert(SingleChooseButton != null);

        SingleOkButton = SingleChooseButton.transform.Find("OkButton").GetComponent<Button>();
        Debug.Assert(SingleOkButton != null);


        MultipleChooseButton = Footer.transform.Find("OkCancel").gameObject;
        Debug.Assert(MultipleChooseButton != null);

        MultipleOkButton = MultipleChooseButton.transform.Find("OkButton").GetComponent<Button>();
        Debug.Assert(MultipleOkButton != null);

        MultipleCancelButton = MultipleChooseButton.transform.Find("CancelButton").GetComponent<Button>();
        Debug.Assert(MultipleCancelButton != null);
    }



}
