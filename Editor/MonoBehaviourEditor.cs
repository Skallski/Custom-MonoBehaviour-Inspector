using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CustomMonoBehaviourInspector.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourEditor : UnityEditor.Editor
    {
        private GUIStyle _iconStyle;
        
        private void OnEnable()
        {
            MonoBehaviourEditorHeaderGUIHandler.OnDrawHeaderGUIEvent += OnHeaderItemGUI;
            MonoBehaviourEditorHeaderGUIHandler.InitializeHeaderGUI(target as MonoBehaviour);
        }

        private void OnDisable()
        {
            MonoBehaviourEditorHeaderGUIHandler.OnDrawHeaderGUIEvent -= OnHeaderItemGUI;
        }

        protected virtual void OnHeaderItemGUI(Rect rect, Object targetObject)
        {
            if (MonoBehaviourEditorSettingsManager.Settings == null)
            {
                return;
            }

            if (MonoBehaviourEditorSettingsManager.Settings.ShowHeader == false)
            {
                return;
            }

            DrawEditScriptButton(rect, targetObject);
            rect.x -= MonoBehaviourEditorHeaderGUIHandler.HEADER_SPACE;
            DrawSerializedMethodsButton(rect, targetObject);
        }

        private static void DrawEditScriptButton(Rect rect, Object targetObject)
        {
            if (MonoBehaviourEditorSettingsManager.Settings.ShowHeaderEditScriptButton == false)
            {
                return;
            }
            
            GUIContent content = new GUIContent(string.Empty, EditorGUIUtility.IconContent("editicon.sml").image,
                "Edit Script");
            
            MonoBehaviourEditorHeaderGUIHandler.DrawHeaderButton(rect, content, () => 
                {
                    MonoScript script = MonoScript.FromMonoBehaviour((MonoBehaviour)targetObject);
                    if (script != null)
                    {
                        AssetDatabase.OpenAsset(script);
                    }
                    else
                    {
                        Debug.LogError($"Cannot open {script.name}");
                    }
                }
            );
        }

        private static void DrawSerializedMethodsButton(Rect rect, Object targetObject)
        {
            if (MonoBehaviourEditorSettingsManager.Settings.ShowHeaderSerializedMethodButton == false)
            {
                return;
            }
            
            GUIContent content = new GUIContent(string.Empty,
                EditorGUIUtility.IconContent("d_Profiler.UIDetails").image,
                "Display Serialized Methods");
            
            MonoBehaviourEditorHeaderGUIHandler.DrawHeaderButton(rect, content, () => 
                {
                    GenericMenu menu = new GenericMenu();
                
                    MethodInfo[] methods = GetSerializedMethods(targetObject);
                    if (methods == null || methods.Length == 0)
                    {
                        menu.AddDisabledItem(new GUIContent("No serialized methods to display"));
                    }
                    else
                    {
                        for (int i = 0, c = methods.Length; i < c; i++)
                        {
                            int index = i;
                            MethodInfo customMethodInfo = methods[i];

                            menu.AddItem(new GUIContent($"{index}: {customMethodInfo.Name}"), false,
                                () => customMethodInfo.Invoke(targetObject, null));
                        }
                    }

                    menu.ShowAsContext();
                }
            );
        }

        public override void OnInspectorGUI()
        {
            if (MonoBehaviourEditorSettingsManager.Settings == null)
            {
                DrawDefaultInspector();
            }
            else
            {
                DrawInspectorFields();
            }
        }

        private void DrawInspectorFields()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();
            
            TooltipAttribute tooltipAttribute = serializedObject.targetObject.GetType().GetCustomAttribute<TooltipAttribute>();
            bool showTooltip = tooltipAttribute != null;
            
            SerializedProperty iterator = serializedObject.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                if (iterator.propertyPath.Equals("m_Script"))
                {
                    if (MonoBehaviourEditorSettingsManager.Settings.ShowScriptField == false)
                    {
                        continue;
                    }

                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.PropertyField(iterator, new GUIContent("Script",
                            showTooltip ? $"{tooltipAttribute.tooltip}" : null), true);
                    }
                }
                else
                {
                    EditorGUILayout.PropertyField(iterator, true);
                }
            }
            
            serializedObject.ApplyModifiedProperties();
            EditorGUI.EndChangeCheck();
        }
        
        private static MethodInfo[] GetSerializedMethods([NotNull] Object targetObj)
        {
            return targetObj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(IsMethodValid)
                .ToArray();

            static bool IsMethodValid([NotNull] MethodInfo m)
            {
                return Attribute.IsDefined(m, typeof(SerializeMethodAttribute)) &&
                       m.GetParameters().Length == 0 &&
                       m.IsAbstract == false &&
                       m.IsStatic == false &&
                       m.ReturnType == typeof(void);
            }
        }
    }
}