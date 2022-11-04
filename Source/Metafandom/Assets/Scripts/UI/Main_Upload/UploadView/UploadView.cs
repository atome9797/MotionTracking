using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UploadView : View
{
    public Button BackButton { get; private set; }

    public Button UploadButton { get; private set; }

    public Button ExitButton { get; private set; }

    public RawImage ThumbnailWindow { get; private set; }

    public List<Toggle> CategoryToggles { get; private set; }

    private void Awake()
    {

        BackButton = transform.Find("BackButton").GetComponent<Button>();
        Debug.Assert(BackButton != null);

        UploadButton = transform.Find("UploadButton").GetComponent<Button>();
        Debug.Assert(BackButton != null);

        ExitButton = transform.Find("ExitButton").GetComponent<Button>();
        Debug.Assert(BackButton != null);

        ThumbnailWindow = transform.Find("ThumbnailWindow").GetComponent<RawImage>();
        Debug.Assert(BackButton != null);

        CategoryToggles = new List<Toggle>();

        CategoryToggles.Add(transform.Find("CategoryDance").GetComponent<Toggle>());
        Debug.Assert(CategoryToggles[0] != null);
        
        CategoryToggles.Add(transform.Find("CategorySing").GetComponent<Toggle>());
        Debug.Assert(CategoryToggles[1] != null);
       
        CategoryToggles.Add(transform.Find("CategoryChallenge").GetComponent<Toggle>());
        Debug.Assert(CategoryToggles[2] != null);   
    }

}
