using PlayFab;
using PlayFab.AdminModels;
using PlayFab.ClientModels;
using PlayFab.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class VideoPost
    {
        public string vimeo_id; //비디오 아이디
        public string user_id; //유저 아이디
        public string video_name; //비디오 이름
        public string video_upload_date; //업로드 날짜
        public List<int> category_id_list = new List<int>();//카테고리 리스트
        public int like_count; //좋아요 수
        public string location; //지역
        public string post_content; //피드 내용
    }


    [Serializable]
    public class VideoCategory
    {
        public int category_id;//카테고리 아이디
        public string category_name;//카테고리 이름
    }


    public static class VideoPostModel
    {
        //재정의 key/value 설정
        public static List<TitleDataKeyValue> TitleDataKeyValueList = new List<TitleDataKeyValue>();
        public static TitleDataKeyValue TitleDataKeyValue = new TitleDataKeyValue();


        public static List<TitleDataKeyValue> SetTitleDataKeyValueList(string key, string value)
        {
            TitleDataKeyValueList.Clear(); //초기화

            TitleDataKeyValue.Key = key;
            TitleDataKeyValue.Value = value;
            TitleDataKeyValueList.Add(TitleDataKeyValue); //key value 배열 리스트에 추가

            return TitleDataKeyValueList;
        }

    }



    public static class CategoryModel
    {
        //카테고리 데이터 선언
        public static VideoCategory VideoCategoryData = new VideoCategory();

        //비디오 포스트 카테고리 리스트 선언
        public static SortedList VideoCategoryDataList = new SortedList();


        /// <summary>
        /// 카테고리 데이터 초기화
        /// </summary>
        public static void InitCategoryData()
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "InitCategoryData"
            };

            PlayFabClientAPI.ExecuteCloudScript(request, OnInitCategoryDataSuccess, OnInitCategoryDataError);
        }

        /// <summary>
        /// 카테고리 데이터 가져오기 성공 콜백 함수
        /// </summary>
        /// <param name="result"></param>
        public static void OnInitCategoryDataSuccess(PlayFab.ClientModels.ExecuteCloudScriptResult result)
        {
            JsonArray jsonArray = new JsonArray();
            jsonArray = (JsonArray)result.FunctionResult;

            for (int i = 0; i < jsonArray.Count; i++)
            {
                JsonObject jsonObject = (JsonObject)jsonArray[i];

                VideoCategoryData = JsonUtility.FromJson<VideoCategory>(jsonObject.ToString());
                VideoCategoryDataList[VideoCategoryData.category_id] = VideoCategoryData.category_name;
            }
        }


        /// <summary>
        ///  카테고리 데이터 가져오기 실패시 콜백 함수
        /// </summary>
        /// <param name="error"></param>
        public static void OnInitCategoryDataError(PlayFabError error)
        {
            Debug.Log("초기화 실패");
        }
    }

}