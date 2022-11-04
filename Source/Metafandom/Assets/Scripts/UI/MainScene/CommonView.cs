using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommonView : View
{
    public Button CallButton { get; private set; }
    
    public GameObject MenuBar { get; private set; }

    public GameObject MessageBox { get; private set; }

    public TextMeshProUGUI MessageText { get; private set; }

    private void Awake()
    {
        MenuBar = transform.Find("MenuBar").gameObject;
        Debug.Assert(MenuBar != null);

        CallButton = MenuBar.transform.Find("CallButton").GetComponent<Button>();
        Debug.Assert(CallButton != null);

        MessageBox = transform.Find("MessageBox").gameObject;
        Debug.Assert(MessageBox != null);

        MessageText = MessageBox.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();

    }
}
