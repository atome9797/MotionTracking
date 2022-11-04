using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterSucceseViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("RegisterSucceseView").GetComponent<RegisterSucceseView>();
        Debug.Assert(View != null);
        Presenter = new RegisterSucceseViewPresenter();
        Debug.Assert(Presenter != null);
    }
}