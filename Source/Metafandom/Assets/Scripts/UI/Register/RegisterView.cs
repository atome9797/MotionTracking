using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterView : View
{
    public TMP_InputField Nickname { get; private set; }
    public TMP_InputField ID  { get; private set; }
    public TMP_InputField Password  { get; private set; }
    public TMP_InputField PasswordCheck  { get; private set; }

    public Button BackButton  { get; private set; }
    public Button JoinButton  { get; private set; }

    public GameObject[] IdGuide  { get; private set; }
    public GameObject[] PasswordGuide  { get; private set; }
    public GameObject[] PasswordCheckGuide  { get; private set; }


    private void Awake()
    {
        Nickname = transform.Find("NicknameInputField").GetComponent<TMP_InputField>();
        Debug.Assert(Nickname != null);
        ID = transform.Find("IdInputField").GetComponent<TMP_InputField>();
        Debug.Assert(ID != null);
        Password = transform.Find("PasswordInputField").GetComponent<TMP_InputField>();
        Debug.Assert(Password != null);
        PasswordCheck = transform.Find("PasswordCheckInputField").GetComponent<TMP_InputField>();
        Debug.Assert(PasswordCheck != null);

        BackButton = transform.Find("BackButton").GetComponent<Button>();
        Debug.Assert(BackButton != null);
        JoinButton = transform.Find("JoinButton").GetComponent<Button>();
        Debug.Assert(JoinButton != null);

        IdGuide = new GameObject[2];

        IdGuide[0] = transform.Find("GuideForAvailableId").gameObject; // 사용 가능한 아이디 입니다.
        Debug.Assert(IdGuide[0] != null);
        IdGuide[1] = transform.Find("GuideForDuplicateId").gameObject; // 이미 사용중인 아이디 입니다.
        Debug.Assert(IdGuide[1] != null);

        PasswordGuide = new GameObject[3];

        PasswordGuide[0] = transform.Find("GuideForAvailablePassword").gameObject; // 사용 가능한 비밀번호 입니다.
        Debug.Assert(PasswordGuide[0] != null);
        PasswordGuide[1] = transform.Find("GuideForMinimumPassword").gameObject; // 비밀번호는 8글자 이상이어야 합니다.
        Debug.Assert(PasswordGuide[1] != null);
        PasswordGuide[2] = transform.Find("GuideForWrongPassword").gameObject; // 영문,숫자, 특수문자(~!@#$%^&*) 조합 8~15자리로 입력해주세요. 
        Debug.Assert(PasswordGuide[2] != null);

        PasswordCheckGuide = new GameObject[2];

        PasswordCheckGuide[0] = transform.Find("GuideForSamePassword").gameObject; // 비밀번호가 일치합니다.
        Debug.Assert(PasswordGuide[0] != null);
        PasswordCheckGuide[1] = transform.Find("GuideForNotSamePassword").gameObject; // 비밀번호가 일치하지 않습니다.
        Debug.Assert(PasswordGuide[1] != null);
        
    }
    public void SetIdGuide(int Num)
    {
        // 상태에 따른 가이드 바꿈
        for (int i = 0; i < IdGuide.Length; i++)
        { IdGuide[i].SetActive(false); }
        // 잘못된 범위일때 상태 표시 제거
        if (Num >= IdGuide.Length)
        { return; }
        
        IdGuide[Num].SetActive(true);
    }
    public void SetPasswordGuide(int Num)
    {
        // 상태에 따른 가이드 바꿈
        for (int i = 0; i < PasswordGuide.Length; i++)
        { PasswordGuide[i].SetActive(false); }
        // 잘못된 범위일때 상태 표시 제거
        if (Num >= PasswordGuide.Length)
        { return; }
        
        PasswordGuide[Num].SetActive(true);
    }
    public void SetPasswordCheckGuide(int Num)
    {
        // 상태에 따른 가이드 바꿈
        for (int i = 0; i < PasswordCheckGuide.Length; i++)
        { PasswordCheckGuide[i].SetActive(false); }
        // 잘못된 범위일때 상태 표시 제거
        if (Num >= PasswordCheckGuide.Length)
        { return; }
        
        PasswordCheckGuide[Num].SetActive(true);
    }
}
