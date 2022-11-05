using UniRx;
using System.Collections.Generic;

namespace Model
{

    public static class UserModel
    {
        public static readonly StringReactiveProperty Id = new StringReactiveProperty();

        //�г���
        public static readonly StringReactiveProperty UserNickName = new StringReactiveProperty();

        //���� ���ƿ� ����Ʈ 
        public static List<StringReactiveProperty> FeedLikeList = new List<StringReactiveProperty>();


        public static void SetId(string id)
        {
            Id.Value = id;
        }

        //�г��� ����
        public static void SetNickName(string nickname)
        {
            UserNickName.Value = nickname;
        }

        /// <summary>
        /// ���� ���ƿ� ����Ʈ ��������
        /// </summary>
        public static void SelectUserFeedLikeList()
        {
            
        }

    }
}
