using UniRx;

namespace Model
{
    public static class CommonViewModel
    {
        public static readonly BoolReactiveProperty ISUploadSceneActive = new BoolReactiveProperty(false);

        public static void setUploadSceneState(bool state)
        {
            ISUploadSceneActive.Value = state;
        }
    }
}