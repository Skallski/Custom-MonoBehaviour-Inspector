using System.Reflection;
using BetterEditorTools.Editor.Utils;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BetterEditorTools.Editor.CustomEditors
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourEditor : UnityEditor.Editor
    {
        private GUIStyle _iconStyle;
        
        private void OnEnable()
        {
            EditorHeaderGuiHandler.OnDrawHeaderGUIEvent += OnHeaderItemGUI;
            EditorHeaderGuiHandler.InitializeHeaderGUI(target as MonoBehaviour);
        }

        private void OnDisable()
        {
            EditorHeaderGuiHandler.OnDrawHeaderGUIEvent -= OnHeaderItemGUI;
        }

        protected virtual void OnHeaderItemGUI(Rect rect, Object targetObject)
        {
            DrawEditButton(rect, targetObject);
            rect.x -= EditorHeaderGuiHandler.HEADER_SPACE;
            DrawMethodsButton(rect, targetObject);
        }

        private void DrawEditButton(Rect rect, Object targetObject)
        {
            GUIContent content = new GUIContent(string.Empty, EditorGUIUtility.IconContent("editicon.sml").image,
                "Edit Script");
            
            EditorHeaderGuiHandler.DrawHeaderButton(rect, content, () => 
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

        private void DrawMethodsButton(Rect rect, Object targetObject)
        {
            GUIContent content = new GUIContent(string.Empty,
                EditorGUIUtility.IconContent("d_Profiler.UIDetails").image,
                "Display Serialized Methods");
            
            EditorHeaderGuiHandler.DrawHeaderButton(rect, content, () => 
                {
                    GenericMenu menu = new GenericMenu();
                
                    MethodInfo[] methods = MethodButtonAttributeHandler.GetObjectMethods(targetObject);
                    if (methods == null || methods.Length == 0)
                    {
                        menu.AddItem(new GUIContent("No serialized methods to display"), false, () => {});
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
            DrawInspectorFields();
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
                    // using (new EditorGUI.DisabledScope(true))
                    // {
                    //     EditorGUILayout.PropertyField(iterator, new GUIContent("Script",
                    //         showTooltip ? $"{tooltipAttribute.tooltip}" : null), true);
                    // }
                    
                    continue;
                }
                else
                {
                    EditorGUILayout.PropertyField(iterator, true);
                }
            }
            
            serializedObject.ApplyModifiedProperties();
            EditorGUI.EndChangeCheck();
        }
    }
}