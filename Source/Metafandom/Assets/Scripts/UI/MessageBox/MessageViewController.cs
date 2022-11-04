using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("MessageView").GetComponent<MessageView>();
        Presenter = new MessageViewPresenter();
    }
}
