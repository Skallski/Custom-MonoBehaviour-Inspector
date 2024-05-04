using System;

namespace BetterEditorTools.PropertyAttributes
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