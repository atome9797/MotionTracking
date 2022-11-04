using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("UserLoginView").GetComponent<LoginView>();
        Debug.Assert(View != null);
        Presenter = new LoginViewPresenter();
        Debug.Assert(Presenter != null);
    }
}