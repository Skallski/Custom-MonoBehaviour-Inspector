using UnityEditor;

namespace CustomMonoBehaviourInspector.Editor
{
    [InitializeOnLoad]
    public static class SetupInitializer
    {
        private const string PACKAGE_NAME = "custom-monobehaviour-inspector";
        
        static SetupInitializer()
        {
            MonoBehaviourEditorSettingsManager.Setup(PACKAGE_NAME);
            
            AssetDatabase.importPackageCompleted += OnImported;
        }
        
        private static void OnImported(string packageName)
        {
            if (packageName.Contains(PACKAGE_NAME))
            {
                SettingsWindow.OpenWindow();
            }
        }
    }
}