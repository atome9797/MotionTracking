using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UploadViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("UploadView").GetComponent<UploadView>();
        Debug.Assert(View != null);
        Presenter = new UploadViewPresenter();
    }


}
