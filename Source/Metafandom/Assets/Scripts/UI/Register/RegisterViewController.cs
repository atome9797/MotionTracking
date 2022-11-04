using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("RegisterView").GetComponent<RegisterView>();
        Debug.Assert(View != null);

        Presenter = new RegisterViewPresenter();
        Debug.Assert(Presenter != null);
    }
}
