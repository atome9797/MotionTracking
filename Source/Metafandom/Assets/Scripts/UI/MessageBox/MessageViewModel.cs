using System.Diagnostics;
using UniRx;

namespace MessageModel
{
    public class MessageViewModel
    {
        
        public static readonly StringReactiveProperty TitleText = new StringReactiveProperty();
        public static readonly StringReactiveProperty Message = new StringReactiveProperty();
        public static readonly IntReactiveProperty MessageBoxStatesValue = new IntReactiveProperty();
        public enum MessageBoxStates { LoginFail = 0, RegisterFail = 1 , None = 2}


        public static void SetMessageBoxText(string HeadText, string MainText, int BoxStates)
        {
            TitleText.Value = HeadText;
            Message.Value = MainText;
            MessageBoxStatesValue.Value = BoxStates;
        }

    }
}
