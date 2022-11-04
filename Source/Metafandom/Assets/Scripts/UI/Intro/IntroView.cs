using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroView : View
{
    public Button LoginButton { get; private set; }
    public Button RegisterButton { get; private set; }

    private void Awake()
    {
        LoginButton = transform.Find("EnterLoginButton").GetComponent<Button>();
        Debug.Assert(LoginButton != null);
        RegisterButton = transform.Find("EnterRegisterButton").GetComponent<Button>();
        Debug.Assert(RegisterButton != null);
    }
}
