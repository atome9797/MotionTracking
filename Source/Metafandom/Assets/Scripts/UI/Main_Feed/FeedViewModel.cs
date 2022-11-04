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
        //���� ����Ʈ ����Ʈ ����
        public static List<VideoPost> PostData;

        //���� ���� ����Ʈ ����
        public static VideoPost PostOneData;
      
        //���� �ε��� ��ȣ
        public static int VideoIndex = 0;
        //�� ���� �ε��� ��ȣ
        public static int VideoTotalIndex = 0;
        //���� �ʱ�ȭ ������ �Ǵ� ����
        public static readonly BoolReactiveProperty VideoInitSuccess = new BoolReactiveProperty();

        //������ �ε��� 5�� ���
        public static int VideoMultipleIndex = 1;


        /// <summary>
        /// ���� ������ �ʱ�ȭ
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
        /// ���� ������ �ʱ�ȭ ������ �ݹ� �Լ�
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
        /// ���� ������ �ʱ�ȭ ���н� �ݹ��Լ�
        /// </summary>
        /// <param name="error"></param>
        public static void OnInitializePostDataError(PlayFabError error)
        {
            Debug.Log("�ʱ�ȭ ����");

        }


        /// <summary>
        /// ���� �����̵� �Ѿ�� ����Ʈ �߰�
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
        /// ���� �����̵� �Ѿ�� ���н� �ݹ��Լ�
        /// </summary>
        /// <param name="error"></param>
        public static void OnNextPostDataError(PlayFabError error)
        {
            Debug.Log("�ʱ�ȭ ����");

        }


        /// <summary>
        /// ���� �����̵� �Ѿ�� ������ �ݹ� �Լ�
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


        //���� ������ ���̵�
        public static int preVideoId()
        {
            //�� ó�� �������� �ƴҶ� ����
            if (VideoIndex > 0)
            {
                VideoIndex -= 1;

                return VideoIndex;
            }
            else // �� ó�� �������϶� -1 ��ȯ
            {
                return -1;
            }
        }

        //���� ������ ���̵�
        public static int nextVideoId()
        {
            //������ �������� �ƴҶ� ����
            if (VideoIndex < VideoTotalIndex)
            {
                VideoIndex += 1;
                return VideoIndex;
            }
            else //�� ������ �������϶� -1 ��ȯ
            {
                return -1;
            }
        }

        //���� �ε��� ������ ���̵�
        public static int currentVideoId()
        {
            return VideoIndex;
        }



    }


}