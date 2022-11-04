using UniRx;

namespace Model
{
    public static class UserModel
    {
        public static readonly StringReactiveProperty Id = new StringReactiveProperty();

        public static void SetId(string id)
        {
            Id.Value = id;
        }
    }
}
