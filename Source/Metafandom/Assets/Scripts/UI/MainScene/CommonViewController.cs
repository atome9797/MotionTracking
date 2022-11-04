using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("CommonView").GetComponent<CommonView>();
        Debug.Assert(View != null);
        Presenter = new CommonParenter();
    }
}
