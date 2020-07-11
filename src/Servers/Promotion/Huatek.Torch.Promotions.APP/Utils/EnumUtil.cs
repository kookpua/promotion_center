using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.APP.Utils
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
