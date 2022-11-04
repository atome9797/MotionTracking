using UniRx;
using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using UnityEngine;

namespace Model
{
    public static class FeedViewModel
    {
        //비디오 포스트 리스트 선언
        public static List<VideoPost> PostData;

        //비디오 개별 포스트 선언
        public static VideoPost PostOneData;
      
        //비디오 인덱스 번호
        public static int VideoIndex = 0;
        //총 비디오 인덱스 번호
        public static int VideoTotalIndex = 0;
        //비디오 초기화 성공시 판단 여부
        public static readonly BoolReactiveProperty VideoInitSuccess = new BoolReactiveProperty();

        //동영상 인덱스 5의 배수
        public static int VideoMultipleIndex = 1;


        /// <summary>
        /// 비디오 데이터 초기화
        /// </summary>
        public static void InitializePostData(int rowSize, string video_id)
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "InitializePostData",
                FunctionParameter = new
                {
                    row = rowSize,
                    videoId = video_id
                }
            };

            PlayFabClientAPI.ExecuteCloudScript(request, OnInitializePostDataSuccess, OnInitializePostDataError);
        }


        /// <summary>
        /// 비디오 데이터 초기화 성공시 콜백 함수
        /// </summary>
        /// <param name="result"></param>
        public static void OnInitializePostDataSuccess(ExecuteCloudScriptResult result)
        {
            PostData = new List<VideoPost>();
            PostOneData = new VideoPost();

            JsonArray jsonArray = new JsonArray();
            jsonArray = (JsonArray)result.FunctionResult;

            for (int i = 0; i < jsonArray.Count; i++)
            {
                JsonObject jsonObject = (JsonObject)jsonArray[i];

                PostOneData = JsonUtility.FromJson<VideoPost>(jsonObject.ToString());
                PostData.Add(PostOneData);
            }
            VideoInitSuccess.Value = true;
            VideoTotalIndex += PostData.Count - 1;

        }

        /// <summary>
        /// 비디오 데이터 초기화 실패시 콜백함수
        /// </summary>
        /// <param name="error"></param>
        public static void OnInitializePostDataError(PlayFabError error)
        {
            Debug.Log("초기화 실패");

        }


        /// <summary>
        /// 다음 슬라이드 넘어갈때 리스트 추가
        /// </summary>
        /// <param name="rowSize"></param>
        /// <param name="indexNumber"></param>
        public static void NextPostData(int rowSize, string video_id)
        {
            var request = new ExecuteCloudScriptRequest
            {
                FunctionName = "InitializePostData",
                FunctionParameter = new
                {
                    row = rowSize,
                    videoId = video_id
                }
            };
            
            PlayFabClientAPI.ExecuteCloudScript(request, OnNextPostDataSuccess, OnNextPostDataError);
        }


        /// <summary>
        /// 다음 슬라이드 넘어가기 실패시 콜백함수
        /// </summary>
        /// <param name="error"></param>
        public static void OnNextPostDataError(PlayFabError error)
        {
            Debug.Log("초기화 실패");

        }


        /// <summary>
        /// 다음 슬라이드 넘어가기 성공시 콜백 함수
        /// </summary>
        /// <param name="result"></param>
        public static void OnNextPostDataSuccess(ExecuteCloudScriptResult result)
        {

            JsonArray jsonArray = new JsonArray();
            jsonArray = (JsonArray)result.FunctionResult;

            if (jsonArray != null)
            {
                for (int i = 0; i < jsonArray.Count; i++)
                {
                    JsonObject jsonObject = (JsonObject)jsonArray[i];

                    PostOneData = JsonUtility.FromJson<VideoPost>(jsonObject.ToString());
                    PostData.Add(PostOneData);
                }
                VideoTotalIndex = PostData.Count - 1;
                VideoMultipleIndex += 1;
            }

        }


        //이전 동영상 아이디
        public static int preVideoId()
        {
            //맨 처음 페이지가 아닐때 실행
            if (VideoIndex > 0)
            {
                VideoIndex -= 1;

                return VideoIndex;
            }
            else // 맨 처음 동영상일때 -1 반환
            {
                return -1;
            }
        }

        //다음 동영상 아이디
        public static int nextVideoId()
        {
            //마지막 페이지가 아닐때 실행
            if (VideoIndex < VideoTotalIndex)
            {
                VideoIndex += 1;
                return VideoIndex;
            }
            else //맨 마지막 동영상일때 -1 반환
            {
                return -1;
            }
        }

        //현재 인덱스 동영상 아이디
        public static int currentVideoId()
        {
            return VideoIndex;
        }



    }


}