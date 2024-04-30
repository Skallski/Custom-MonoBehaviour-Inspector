using System;
using UnityEditor;

namespace BetterEditorTools.Editor.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static object GetValue(this SerializedProperty property)
        {
            object obj = property.serializedObject.targetObject;
 
            System.Reflection.FieldInfo field = null;
            foreach (string path in property.propertyPath.Split('.'))
            {
                Type type = obj.GetType();
                field = type.GetField(path);
                obj = field.GetValue(obj);
            }
            
            return obj;
        }
    }
}