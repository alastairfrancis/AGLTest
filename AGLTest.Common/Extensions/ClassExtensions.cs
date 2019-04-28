using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AGLTest.Common.Extensions
{
    public static class ClassExtensions
    {
        /// <summary>
        /// Get all concrete classes that implement the specified interface
        /// </summary>
        public static IEnumerable<Type> GetImplementedClasses(this Type interfaceType)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
        }

        /// <summary>
        /// Get all field info for public properties
        /// </summary>
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }
    }
}
