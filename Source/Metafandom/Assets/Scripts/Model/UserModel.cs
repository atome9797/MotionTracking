using UniRx;
using System.Collections.Generic;

namespace Model
{

    public static class UserModel
    {
        public static readonly StringReactiveProperty Id = new StringReactiveProperty();

        //닉네임
        public static readonly StringReactiveProperty UserNickName = new StringReactiveProperty();

        //유저 좋아요 리스트 
        public static List<StringReactiveProperty> FeedLikeList = new List<StringReactiveProperty>();


        public static void SetId(string id)
        {
            Id.Value = id;
        }

        //닉네임 설정
        public static void SetNickName(string nickname)
        {
            UserNickName.Value = nickname;
        }

        /// <summary>
        /// 유저 좋아요 리스트 가져오기
        /// </summary>
        public static void SelectUserFeedLikeList()
        {
            
        }

    }
}
