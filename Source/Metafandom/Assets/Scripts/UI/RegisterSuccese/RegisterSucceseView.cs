using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RegisterSucceseView : View
{
    public Button RegisterSucceseButton { get; private set; }

    private void Awake()
    {
        RegisterSucceseButton = transform.Find("MoveForMainSceenButton").GetComponent<Button>();
        Debug.Assert(RegisterSucceseButton != null);
    }
}
