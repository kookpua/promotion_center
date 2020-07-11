using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.APP.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举中的Description
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nameInstead"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute?.Description;
        }
    }
}
