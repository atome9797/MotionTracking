using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Model
{
    public static class UploadSeneModel
    {
        public static readonly BoolReactiveProperty IsUploadViewActive = new BoolReactiveProperty();
        public static readonly StringReactiveProperty Path = new StringReactiveProperty();
        public static readonly BoolReactiveProperty IsEncording = new BoolReactiveProperty(true);
        public static readonly StringReactiveProperty uploadState = new StringReactiveProperty();
        public static readonly ReactiveProperty<Texture2D> videoThumbnail = new ReactiveProperty<Texture2D>();


        public static bool[] setcategory;
        public static List<int> Category = new List<int>();

        public static void initcategory()
        {
            if (setcategory == null)
                setcategory = new bool[CategoryModel.VideoCategoryDataList.Count];
            else
            {
                for (int i = 0; i < setcategory.Length; ++i)
                    setcategory[i] = false;
                Category.Clear();
            }
        }

        public static void chageCategory()
        {
            for (int i = 0; i < setcategory.Length; ++i)
            {
                if (setcategory[i])
                {
                    int temp = (int)CategoryModel.VideoCategoryDataList.GetKey(i);
                    Category.Add(temp);
                }
            }
        }

        public static void ChangeView(bool active)
        {
            IsUploadViewActive.Value = active;
        }

        public static void LoadVideo(string path)
        {
            if (!string.IsNullOrEmpty(Path.Value))
                Path.Value = null;
            Path.Value = path;
        }

        public static void ChangeEncodingState(bool State)
        {
            IsEncording.Value = State;
        }

        public static void changeUploadState(string state)
        {
            uploadState.Value = state;
        }

        public static void SetThumbnail(Texture2D thumbnail)
        {
            videoThumbnail.Value = thumbnail;
        }

    }
}
