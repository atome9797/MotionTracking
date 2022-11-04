using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Model
{
    public static class LoginViewModel
    {
        public static readonly StringReactiveProperty Email = new StringReactiveProperty();
        public static readonly StringReactiveProperty Password = new StringReactiveProperty();
        
        public static void SetEmailText(string text)
        {
            Email.Value = text;
        }
        public static void SetPasswordText(string text)
        {
            Password.Value = text;
        }

        public static void InitText()
        {
            Email.Value = "";
            Password.Value = "";
        }

    }
}