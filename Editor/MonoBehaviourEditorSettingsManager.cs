using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CustomMonoBehaviourInspector.Editor
{
    [InitializeOnLoad]
    public static class MonoBehaviourEditorSettingsManager
    {
        private const string PROJECT_SETTINGS_PATH = "Project/Custom MonoBehaviour Inspector";
        
        internal static MonoBehaviourEditorSettings Settings { get; private set; }
        
        static MonoBehaviourEditorSettingsManager()
        {
            Settings = MonoBehaviourEditorSettings.GetOrCreateInstance();
        }

        [MenuItem("Window/Custom MonoBehaviour Inspector/Settings")]
        public static void OpenCustomMonoBehaviourInspectorSettings()
        {
            SettingsService.OpenProjectSettings(PROJECT_SETTINGS_PATH);
        }
        
        [SettingsProvider]
        public static SettingsProvider CreateCustomMonoBehaviourInspectorSettingsProvider()
        {
            SettingsProvider provider = new SettingsProvider(PROJECT_SETTINGS_PATH, SettingsScope.Project)
            {
                label = "Custom MonoBehaviour Inspector",
                guiHandler = _ =>
                {
                    SerializedObject settings = new SerializedObject(MonoBehaviourEditorSettings.GetOrCreateInstance());
                    
                    EditorGUILayout.Space();
                    
                    DrawPropertyWithOffset("<ShowScriptField>k__BackingField", "Show Script Field");
                    DrawPropertyWithOffset("<ShowHeader>k__BackingField", "Show Header");
                    DrawPropertyWithOffset("<ShowHeaderSerializedMethodButton>k__BackingField", "Show Header Serialized Method Button");
                    DrawPropertyWithOffset("<ShowHeaderEditScriptButton>k__BackingField", "Show Header Edit Script Button");

                    settings.ApplyModifiedPropertiesWithoutUndo();

                    // LOCAL METHOD
                    // Draw a property field with offset
                    void DrawPropertyWithOffset(string propertyName, string displayName)
                    {
                        const float leftOffset = 8;

                        Rect rect = EditorGUILayout.GetControlRect();
                        rect.x += leftOffset;
                        rect.width -= leftOffset;
                        EditorGUI.PropertyField(rect, settings.FindProperty(propertyName), new GUIContent(displayName));
                    }
                },
                keywords = new HashSet<string>(new[]
                {
                    "Show Script Field", 
                    "Show Header",
                    "Show Header Serialized Method Button",
                    "Show Header Edit Script Button"
                })
            };

            return provider;
        }
    }
}