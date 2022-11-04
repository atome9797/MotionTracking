using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("IntroView").GetComponent<IntroView>();
        Debug.Assert(View != null);
        Presenter = new IntroViewPresenter();
        Debug.Assert(Presenter != null);
    }
}