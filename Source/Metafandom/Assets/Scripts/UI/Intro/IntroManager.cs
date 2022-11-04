using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject[] IntroPagePrefab = new GameObject[0];

    static public GameObject[] IntroPage;

    public enum PageName { IntroPage = 0, LoginPage = 1, RegisterPage = 2, RegisterSuccessPage = 3, MessageBox = 4 }

    private void Awake()
    {
        IntroPage = new GameObject[IntroPagePrefab.Length];
        for (int i = 0; i < IntroPagePrefab.Length; i++)
        {
            IntroPage[i] = Instantiate(IntroPagePrefab[i]);
        }
        MovePage(0);
    }

    public static void MovePage(int Page)
    {
        for (int i = 0; i < IntroPage.Length; i++)
        { if (IntroPage[i].activeSelf) { IntroPage[i].SetActive(false); } }
        IntroPage[Page].SetActive(true);
    }

}