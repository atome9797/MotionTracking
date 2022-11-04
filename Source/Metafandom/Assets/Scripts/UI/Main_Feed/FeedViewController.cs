using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("FeedView").GetComponent<FeedView>();
        Debug.Assert(View != null);
        Presenter = new FeedViewPresenter();
        Debug.Assert(Presenter != null);

    }

}
