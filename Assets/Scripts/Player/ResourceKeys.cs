namespace Constants
{
    public static partial class ResourceKeys
    {
        private const string BasePath = "Assets/Bundles/";
        private const string PrefabSuffix = ".prefab";
        public static class Player
        {
            private const string ResourcePath = BasePath + "Player/";
            public const string PlayerPrefab = ResourcePath + "player" + PrefabSuffix;
        }
    }
}