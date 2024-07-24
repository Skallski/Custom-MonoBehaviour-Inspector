using UnityEditor;

namespace CustomMonoBehaviourInspector.Editor
{
    [InitializeOnLoad]
    public static class MonoBehaviourEditorSettingsManager
    {
        internal const string SETTINGS_ASSET_PATH = 
            "Packages/com.skallu.custom-monobehaviour-inspector/Settings/MonoBehaviourEditorSettings.asset";

        public static MonoBehaviourEditorSettings Settings { get; internal set; }

        static MonoBehaviourEditorSettingsManager()
        {
            Settings = AssetDatabase.LoadAssetAtPath<MonoBehaviourEditorSettings>(SETTINGS_ASSET_PATH);
        }
    }
}