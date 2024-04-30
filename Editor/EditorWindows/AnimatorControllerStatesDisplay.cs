using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace BetterEditorTools.Editor.EditorWindows
{
    public class AnimatorControllerStatesDisplay : EditorWindow
    {
        private AnimatorController _animationController;
        private Vector2 _scrollPosition = Vector2.zero;

        [MenuItem("Window/Animation/Animator Controller States Display")]
        private static void OpenWindow()
        {
            AnimatorControllerStatesDisplay window = GetWindow<AnimatorControllerStatesDisplay>();
            window.titleContent = new GUIContent("Animator Controller States Display");
            window.minSize = new Vector2(275, 400);
            window.Show();
        }

        private void OnGUI()
        {
            _animationController = EditorGUILayout.ObjectField("Animator Controller", _animationController, 
                typeof(AnimatorController), false) as AnimatorController;

            if (_animationController != null)
            {
                EditorGUILayout.Space();

                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
                {
                    AnimatorControllerLayer[] layers = _animationController.layers;
                    foreach (AnimatorControllerLayer layer in layers)
                    {
                        GUILayout.Label($"{layer.name}:", EditorStyles.boldLabel);
                    
                        AnimatorStateMachine stateMachine = layer.stateMachine;
                        ChildAnimatorState[] states = stateMachine.states;
                    
                        foreach (ChildAnimatorState childState in states)
                        {
                            AnimatorState state = childState.state;
                        
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField($"{state.name}: {state.nameHash}");
                                if (GUILayout.Button("copy", GUILayout.Width(50)))
                                {
                                    EditorGUIUtility.systemCopyBuffer = state.nameHash.ToString();
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                    
                        EditorGUILayout.Space();
                    }
                }
                EditorGUILayout.EndScrollView();
            }
        }
    }
}