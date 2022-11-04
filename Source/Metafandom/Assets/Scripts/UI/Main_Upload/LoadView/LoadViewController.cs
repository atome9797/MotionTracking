using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("LoadView").GetComponent<LoadView>();
        Debug.Assert(View != null);
        Presenter = new LoadViewPresenter();
    }

}
