using UnityEditor;
using UnityEngine;

namespace CustomMonoBehaviourInspector.Editor
{
    public class MonoBehaviourEditorSettings : ScriptableObject
    {
        private const string ASSET_PATH = 
            "Packages/com.Skallski.custom-monobehaviour-inspector/Settings/MonoBehaviourEditorSettings.asset";
        
        [field: SerializeField] internal bool ShowScriptField { get; set; }
        [field: SerializeField] internal bool ShowHeader { get; set; }
        [field: SerializeField] internal bool ShowHeaderSerializedMethodButton { get; set; }
        [field: SerializeField] internal bool ShowHeaderEditScriptButton { get; set; }

        private void SetDefaults()
        {
            ShowScriptField = false;
            ShowHeader = true;
            ShowHeaderSerializedMethodButton = true;
            ShowHeaderEditScriptButton = true;
        }

        internal static MonoBehaviourEditorSettings GetOrCreateInstance()
        {
            MonoBehaviourEditorSettings settings = AssetDatabase.LoadAssetAtPath<MonoBehaviourEditorSettings>(ASSET_PATH);
            if (settings == null)
            {
                settings = CreateInstance<MonoBehaviourEditorSettings>();
                settings.SetDefaults();
                
                AssetDatabase.CreateAsset(settings, ASSET_PATH);
                AssetDatabase.SaveAssets();
            }
            
            return settings;
        }
    }
}