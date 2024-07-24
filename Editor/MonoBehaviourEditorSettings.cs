using UnityEngine;

namespace CustomMonoBehaviourInspector.Editor
{
    public class MonoBehaviourEditorSettings : ScriptableObject
    {
        [field: SerializeField] internal bool ShowScriptField { get; private set; }
        [field: SerializeField] internal bool ShowHeader { get; private set; }
        [field: SerializeField] internal bool ShowHeaderSerializedMethodButton { get; private set; }
        [field: SerializeField] internal bool ShowHeaderEditScriptButton { get; private set; }

        internal void Setup(bool showScriptField, bool showHeader, bool showHeaderSerializedMethodButton, 
            bool showHeaderEditScriptButton)
        {
            ShowScriptField = showScriptField;
            ShowHeader = showHeader;
            ShowHeaderSerializedMethodButton = showHeaderSerializedMethodButton;
            ShowHeaderEditScriptButton = showHeaderEditScriptButton;
        }
    }
}