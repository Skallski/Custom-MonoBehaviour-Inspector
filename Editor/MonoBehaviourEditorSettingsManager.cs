using UnityEditor;

namespace CustomMonoBehaviourInspector.Editor
{
    [InitializeOnLoad]
    public static class MonoBehaviourEditorSettingsManager
    {
        private const string PACKAGE_NAME = "custom-monobehaviour-inspector";
        
        internal static MonoBehaviourEditorSettings Settings { get; private set; }

        static MonoBehaviourEditorSettingsManager()
        {
            // setup
            string assetPath = $"Packages/com.skallu.{PACKAGE_NAME}/Settings/MonoBehaviourEditorSettings.asset";
            Settings = AssetDatabase.LoadAssetAtPath<MonoBehaviourEditorSettings>(assetPath);
        }
    }
}