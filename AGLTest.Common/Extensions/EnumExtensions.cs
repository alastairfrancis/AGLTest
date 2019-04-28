using System;
using System.ComponentModel;

namespace AGLTest.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get display name attribute of enum 
        /// </summary>
        public static string GetDisplayName(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false) as DisplayNameAttribute[];

            if (descriptionAttributes != null && descriptionAttributes.Length > 0)
            {
                return descriptionAttributes[0].DisplayName;
            }

            return value.ToString();
        }

        /// <summary>
        /// Convert string value to enum
        /// </summary>
        public static T ToEnum<T>(this string val)
        {
            return (T)Enum.Parse(typeof(T), val);
        }
    }
}
