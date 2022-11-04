using PlayFab;
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
        public string vimeo_id; //���� ���̵�
        public string user_id; //���� ���̵�
        public string user_name; //���� �̸�
        public int index; //�ε���
        public string video_name; //���� �̸�
        public string video_upload_date; //���ε� ��¥
        public List<int> category_id_list = new List<int>();//ī�װ� ����Ʈ
    }
    [Serializable]
    public class VideoCategory
    {
        public int category_id;//ī�װ� ���̵�
        public string category_name;//ī�װ� �̸�
    }
    public static class CategoryModel
    {
        //ī�װ� ������ ����
        public static VideoCategory VideoCategoryData = new VideoCategory();

        //���� ����Ʈ ī�װ� ����Ʈ ����
        public static SortedList VideoCategoryDataList = new SortedList();


        /// <summary>
        /// ī�װ� ������ �ʱ�ȭ
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
        /// ī�װ� ������ �������� ���� �ݹ� �Լ�
        /// </summary>
        /// <param name="result"></param>
        public static void OnInitCategoryDataSuccess(ExecuteCloudScriptResult result)
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
        ///  ī�װ� ������ �������� ���н� �ݹ� �Լ�
        /// </summary>
        /// <param name="error"></param>
        public static void OnInitCategoryDataError(PlayFabError error)
        {
            Debug.Log("�ʱ�ȭ ����");
        }
    }

}