using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public static class EnumExtensions
    {
        public static string GetDisplayAttributeFrom(this Enum enumValue, Type enumType)
        {
            string displayName = "";
            MemberInfo info = enumType.GetMember(enumValue.ToString()).First();

            if (info != null && info.CustomAttributes.Any())
            {
                StringValueAttribute nameAttr = info.GetCustomAttribute<StringValueAttribute>();

                if (nameAttr != null)
                {
                    // Check for localization
                    if (nameAttr.Type != null && nameAttr.Value != null)
                    {
                        // I recommend not newing this up every time for performance
                        // but rather use a global instance or pass one in
                        var manager = new ResourceManager(nameAttr.Type);
                        displayName = nameAttr.Value;
                    }
                    else if (nameAttr.Value != null)
                    {
                        displayName = nameAttr != null ? nameAttr.Value : enumValue.ToString();
                    }
                }
            }
            else
            {
                displayName = enumValue.ToString();
            }
            return displayName;
        }
    }
}
