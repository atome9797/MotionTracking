using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : View
{
    public TMP_InputField Id { get; private set; }
    public TMP_InputField Password { get; private set; }

    public Button LoginButton { get; private set; }
    public Button RegisterButton { get; private set; }
    public Button LoginChoiceButton { get; private set; }

    private void Awake()
    {
        Id = transform.Find("IDInputField").GetComponent<TMP_InputField>();
        Debug.Assert(Id != null);
        Password = transform.Find("PasswordInputField").GetComponent<TMP_InputField>();
        Debug.Assert(Password != null);

        LoginButton = transform.Find("LoginButton").GetComponent<Button>();
        Debug.Assert(LoginButton != null);
        RegisterButton = transform.Find("SignUpButton").GetComponent<Button>();
        Debug.Assert(RegisterButton != null);
        LoginChoiceButton = transform.Find("BackButton").GetComponent<Button>();
        Debug.Assert(LoginChoiceButton != null);
    }
}
