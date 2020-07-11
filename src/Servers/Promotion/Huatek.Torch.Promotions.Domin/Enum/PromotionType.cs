using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Domain.Enum
{
    /// <summary>
    /// 活动类型
    /// </summary>
    public enum PromotionType
    {
        /// <summary>
        /// 限时折扣
        /// </summary>
        [Description("限时折扣")]
        LimitDiscount = 1,
        /// <summary>
        /// 新用户专享
        /// </summary>
        [Description("新用户专享")]
        NewUser =2,

    }
}
