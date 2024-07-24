using System;
using UnityEditor;
using UnityEngine;

namespace CustomMonoBehaviourInspector.Editor
{
    public class SettingsWindow : EditorWindow
    {
        private bool _showScriptField;
        private bool _showHeader;
        private bool _showHeaderSerializedMethodButton;
        private bool _showHeaderEditScriptButton;
        
        [MenuItem("Custom MonoBehaviour Inspector/Settings")]
        internal static void OpenWindow()
        {
            SettingsWindow window = GetWindow<SettingsWindow>();
            window.titleContent = new GUIContent("Custom MonoBehaviour Settings");
            window.Show();
        }

        private void OnEnable()
        {
            if (MonoBehaviourEditorSettingsManager.Settings != null)
            {
                MonoBehaviourEditorSettingsManager.Settings.Setup(_showScriptField, _showHeader,
                    _showHeaderSerializedMethodButton, _showHeaderEditScriptButton);
            }
            else
            {
                MonoBehaviourEditorSettings instance = ScriptableObject.CreateInstance<MonoBehaviourEditorSettings>();
                instance.Setup(_showScriptField, _showHeader, _showHeaderSerializedMethodButton, _showHeaderEditScriptButton);
                
                AssetDatabase.CreateAsset(instance, MonoBehaviourEditorSettingsManager.SETTINGS_ASSET_PATH);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                MonoBehaviourEditorSettingsManager.Settings = instance;
            }
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            _showScriptField = EditorGUILayout.Toggle(
                new GUIContent("Show Script Field"), _showScriptField);
            
            _showHeader = EditorGUILayout.Toggle(
                new GUIContent("Show Header"), _showHeader);
            
            _showHeaderSerializedMethodButton = EditorGUILayout.Toggle(
                new GUIContent("Show Header SerializedMethods Button"), _showHeaderSerializedMethodButton);
            
            _showHeaderEditScriptButton = EditorGUILayout.Toggle(
                new GUIContent("Show Header EditScript Button"), _showHeaderEditScriptButton);
            
            if (EditorGUI.EndChangeCheck())
            {
                if (MonoBehaviourEditorSettingsManager.Settings != null)
                {
                    MonoBehaviourEditorSettingsManager.Settings.Setup(_showScriptField, _showHeader,
                        _showHeaderSerializedMethodButton, _showHeaderEditScriptButton);
                }
            }

            EditorGUILayout.Space();
            
            if (GUILayout.Button("Save Settings"))
            {
                Close();
            }
        }
    }
}