using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MessageViewPresenter : Presenter
{
    private MessageView _MessageView;
    private CompositeDisposable _compositeDisposable = new CompositeDisposable();
    
    int returnValue = 0;
    
    public override void OnInitialize(View view)
    {
        _MessageView = view as MessageView;
        _MessageView.TitleText.text = "Error";
        _MessageView.Message.text = "";

        _MessageView.SingleChooseButton.SetActive(false);
        _MessageView.MultipleChooseButton.SetActive(false);
        InitializeRx();
        //OnClose();
    }
    public override void OnRelease()
    {
        _MessageView = default;
        _compositeDisposable.Dispose();
    }
    protected override void OnOccuredUserEvent()
    {
        _MessageView.SingleOkButton.OnClickAsObservable().Subscribe(_=> OnClickButton("Ok")).AddTo(_compositeDisposable);
        _MessageView.MultipleOkButton.OnClickAsObservable().Subscribe(_=> OnClickButton("Ok")).AddTo(_compositeDisposable);
        _MessageView.MultipleCancelButton.OnClickAsObservable().Subscribe(_=> OnClickButton("Cancel")).AddTo(_compositeDisposable);


        MessageModel.MessageViewModel.MessageBoxStatesValue.Subscribe(MessageBox).AddTo(_compositeDisposable);

    }
    protected override void OnUpdatedModel()
    {
    }

    private void OnClickButton(string buttonType)
    {

        int MessageBoxStates = int.Parse(MessageModel.MessageViewModel.MessageBoxStatesValue.ToString());

        if (MessageBoxStates == (int)MessageModel.MessageViewModel.MessageBoxStates.RegisterFail)
        {
            //중단일 경우 확인 버튼 누르면 인트로 페이지로 이동
            if(buttonType == "Ok")
            {
                Model.RegisterViewModel.Init();
                IntroManager.MovePage((int)IntroManager.PageName.IntroPage);
            }
        }
        

        OnClose();
    }

    private void MessageBox(int MessageBoxState)
    {
        //회원가입 메세지면
        if(MessageBoxState == (int)MessageModel.MessageViewModel.MessageBoxStates.RegisterFail)
        {
            Debug.Log("회원가입");
            _MessageView.TitleText.text = MessageModel.MessageViewModel.TitleText.ToString();
            _MessageView.Message.text = MessageModel.MessageViewModel.Message.ToString();
            _MessageView.SingleChooseButton.SetActive(false);
            _MessageView.MultipleChooseButton.SetActive(true);
        }
        else if(MessageBoxState == (int)MessageModel.MessageViewModel.MessageBoxStates.LoginFail)
        {
            Debug.Log("로그인");
            _MessageView.TitleText.text = MessageModel.MessageViewModel.TitleText.ToString();
            _MessageView.Message.text = MessageModel.MessageViewModel.Message.ToString();
            _MessageView.SingleChooseButton.SetActive(true);
            _MessageView.MultipleChooseButton.SetActive(false);
        }

    }


    public int GetReturnValue()
    {
        return returnValue;
    }
    public void OnClose()
    {
        IntroManager.IntroPage[(int)IntroManager.PageName.MessageBox].SetActive(false);
    }



}
