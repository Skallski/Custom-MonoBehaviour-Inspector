using System;

namespace CustomMonoBehaviourInspector
{
    [AttributeUsage(AttributeTargets.Method)]
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public class SerializeMethodAttribute : Attribute
    {
        public SerializeMethodAttribute()
        {
            
        }
    }
}