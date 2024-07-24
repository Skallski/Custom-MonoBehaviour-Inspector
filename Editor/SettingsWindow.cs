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
        
        [MenuItem("Window/Custom Inspector/MonoBehaviour Inspector Settings")]
        private static void OpenWindow()
        {
            SettingsWindow window = GetWindow<SettingsWindow>();
            window.titleContent = new GUIContent("MonoBehaviour Inspector Settings", 
                EditorGUIUtility.IconContent("d__Popup@2x").image);
            
            window.Show();
        }

        private void OnEnable()
        {
            _showScriptField = MonoBehaviourEditorSettingsManager.Settings.ShowScriptField;
            _showHeader = MonoBehaviourEditorSettingsManager.Settings.ShowHeader;
            _showHeaderSerializedMethodButton = MonoBehaviourEditorSettingsManager.Settings.ShowHeaderSerializedMethodButton;
            _showHeaderEditScriptButton = MonoBehaviourEditorSettingsManager.Settings.ShowHeaderEditScriptButton;
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
                MonoBehaviourEditorSettingsManager.Settings.Setup(_showScriptField, _showHeader,
                    _showHeaderSerializedMethodButton, _showHeaderEditScriptButton);
            }

            EditorGUILayout.Space();
            
            if (GUILayout.Button("Save Settings"))
            {
                Close();
            }
        }
    }
}