using UnityEditor;

namespace CustomMonoBehaviourInspector.Editor
{
    [InitializeOnLoad]
    public static class SetupInitializer
    {
        private const string PACKAGE_NAME = "custom-monobehaviour-inspector";

        static SetupInitializer()
        {
            AssetDatabase.importPackageCompleted += OnImported;
        }
        
        private static void OnImported(string packageName)
        {
            if (packageName.Contains(PACKAGE_NAME))
            {
                Setup();
            }
        }

        private static void Setup()
        {
            SettingsWindow.OpenWindow();
        }
    }
}