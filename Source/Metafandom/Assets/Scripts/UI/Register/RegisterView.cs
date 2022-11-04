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

        IdGuide[0] = transform.Find("GuideForAvailableId").gameObject; // ��� ������ ���̵� �Դϴ�.
        Debug.Assert(IdGuide[0] != null);
        IdGuide[1] = transform.Find("GuideForDuplicateId").gameObject; // �̹� ������� ���̵� �Դϴ�.
        Debug.Assert(IdGuide[1] != null);

        PasswordGuide = new GameObject[3];

        PasswordGuide[0] = transform.Find("GuideForAvailablePassword").gameObject; // ��� ������ ��й�ȣ �Դϴ�.
        Debug.Assert(PasswordGuide[0] != null);
        PasswordGuide[1] = transform.Find("GuideForMinimumPassword").gameObject; // ��й�ȣ�� 8���� �̻��̾�� �մϴ�.
        Debug.Assert(PasswordGuide[1] != null);
        PasswordGuide[2] = transform.Find("GuideForWrongPassword").gameObject; // ����,����, Ư������(~!@#$%^&*) ���� 8~15�ڸ��� �Է����ּ���. 
        Debug.Assert(PasswordGuide[2] != null);

        PasswordCheckGuide = new GameObject[2];

        PasswordCheckGuide[0] = transform.Find("GuideForSamePassword").gameObject; // ��й�ȣ�� ��ġ�մϴ�.
        Debug.Assert(PasswordGuide[0] != null);
        PasswordCheckGuide[1] = transform.Find("GuideForNotSamePassword").gameObject; // ��й�ȣ�� ��ġ���� �ʽ��ϴ�.
        Debug.Assert(PasswordGuide[1] != null);
        
    }
    public void SetIdGuide(int Num)
    {
        // ���¿� ���� ���̵� �ٲ�
        for (int i = 0; i < IdGuide.Length; i++)
        { IdGuide[i].SetActive(false); }
        // �߸��� �����϶� ���� ǥ�� ����
        if (Num >= IdGuide.Length)
        { return; }
        
        IdGuide[Num].SetActive(true);
    }
    public void SetPasswordGuide(int Num)
    {
        // ���¿� ���� ���̵� �ٲ�
        for (int i = 0; i < PasswordGuide.Length; i++)
        { PasswordGuide[i].SetActive(false); }
        // �߸��� �����϶� ���� ǥ�� ����
        if (Num >= PasswordGuide.Length)
        { return; }
        
        PasswordGuide[Num].SetActive(true);
    }
    public void SetPasswordCheckGuide(int Num)
    {
        // ���¿� ���� ���̵� �ٲ�
        for (int i = 0; i < PasswordCheckGuide.Length; i++)
        { PasswordCheckGuide[i].SetActive(false); }
        // �߸��� �����϶� ���� ǥ�� ����
        if (Num >= PasswordCheckGuide.Length)
        { return; }
        
        PasswordCheckGuide[Num].SetActive(true);
    }
}
