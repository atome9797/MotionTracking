using PlayFab.MultiplayerModels;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UniRx;
using UnityEngine;
namespace Model
{
    public static class RegisterViewModel
    {
        public static readonly StringReactiveProperty Nickname = new StringReactiveProperty();
        public static readonly StringReactiveProperty ID = new StringReactiveProperty();
        public static readonly StringReactiveProperty Password = new StringReactiveProperty();
        public static readonly StringReactiveProperty PasswordCheck = new StringReactiveProperty();

        public static readonly IntReactiveProperty PasswordCheckStatus = new IntReactiveProperty();
        public static readonly IntReactiveProperty IDStatus = new IntReactiveProperty();
        public static readonly IntReactiveProperty PasswordStatus = new IntReactiveProperty();

        public enum IdStatusEnum { Enable = 0, Disable = 1 }
        public enum PasswordCheckStatusEnum { Enable = 0, Disable = 1}
        public enum PasswordStatusEnum { PasswordMatch = 0  , ShortPassword = 1, PasswordNotMatch = 2 }

        public static void Init()
        {
            Nickname.Value = "";
            ID.Value = "";
            Password.Value = "";
            PasswordCheck.Value = "";
        }

        public static void SetNicknameText(string text)
        {
            Nickname.Value = text;
        }
        public static void SetIDText(string text)
        {
            ID.Value = text;
            IDFormat();
        }
        public static void SetPasswordText(string text)
        {
            Password.Value = text;
            PasswordFormat();
        }
        public static void SetPasswordCheckText(string text)
        {
            PasswordCheck.Value = text;
            PasswordCheckFormat();
        }

        private static void IDFormat()
        {
            Regex regexPass = new Regex(@"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?", RegexOptions.IgnorePatternWhitespace);
            bool isMatchEn = regexPass.IsMatch(ID.Value);

            if (isMatchEn)
            {
                SetIDStatus((int)IdStatusEnum.Enable);
            }
            else
            {
                SetIDStatus((int)IdStatusEnum.Disable);
            }
        }
        private static void PasswordFormat()
        {
            PasswordCheckFormat();
            // 비밀번호 자릿수 체크
            if (Password.Value.Length < 8)
            { SetPasswordStatus((int)PasswordStatusEnum.ShortPassword); return; }

            Regex engRegex = new Regex(@"[a-zA-Z]");
            bool isMatchEng = engRegex.IsMatch(Password.Value);
            Regex numRegex = new Regex(@"[0-9]");
            bool isMatchNum = numRegex.IsMatch(Password.Value);
            Regex specialRegex = new Regex(@"[~!@\#$%^&*\()\=+|\\/:;?""<>']");
            bool isMatchSpecial = specialRegex.IsMatch(Password.Value);

            if (isMatchEng && isMatchNum && isMatchSpecial)
            { SetPasswordStatus((int)PasswordStatusEnum.PasswordMatch); }
            else
            { SetPasswordStatus((int)PasswordStatusEnum.PasswordNotMatch); }
        }
        private static void PasswordCheckFormat()
        {
            // 비밀번호 같은지 확인
            if (PasswordCheck.Value != Password.Value)
            {
                SetPasswordCheckStatus((int)PasswordCheckStatusEnum.Disable);
            }
            else
            {
                SetPasswordCheckStatus((int)PasswordCheckStatusEnum.Enable);
            }
        }
        // 각 가입조건 체크
        public static void SetIDStatus(int Number)
        {
            IDStatus.Value = Number;
        }
        public static void SetPasswordStatus(int Number)
        {
            PasswordStatus.Value = Number;
        }
        public static void SetPasswordCheckStatus(int Number)
        {
            PasswordCheckStatus.Value = Number;
        }
    }
}