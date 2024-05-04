using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace BetterEditorTools.Editor.Utils
{
    public static class MethodButtonAttributeHandler
    {
        private static bool IsMethodValid([NotNull] MethodInfo m)
        {
            return Attribute.IsDefined(m, typeof(PropertyAttributes.SerializeMethodAttribute)) &&
                   m.GetParameters().Length == 0 &&
                   m.IsAbstract == false &&
                   m.IsStatic == false &&
                   m.ReturnType == typeof(void);
        }
        
        public static MethodInfo[] GetObjectMethods([NotNull] Object targetObj)
        {
            return targetObj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(IsMethodValid)
                    .ToArray();
        }
    }
}