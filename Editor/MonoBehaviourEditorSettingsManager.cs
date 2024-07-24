using UnityEditor;

namespace CustomMonoBehaviourInspector.Editor
{
    public static class MonoBehaviourEditorSettingsManager
    {
        public static MonoBehaviourEditorSettings Settings { get; private set; }
        
        internal static void Setup(string packageName)
        {
            string assetPath = $"Packages/com.skallu.{packageName}/Settings/MonoBehaviourEditorSettings.asset";
            Settings = AssetDatabase.LoadAssetAtPath<MonoBehaviourEditorSettings>(assetPath);
        }
    }
}