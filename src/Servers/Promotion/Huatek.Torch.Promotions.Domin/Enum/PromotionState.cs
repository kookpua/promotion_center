using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Domain.Enum
{
    /// <summary>
    /// 活动状态
    /// </summary>
    public enum PromotionState
    {
        /// <summary>
        /// 已创建
        /// </summary>
        [Description("已创建")]
        Created = 1,
        /// <summary>
        /// 已发布
        /// </summary>
        [Description("已发布")]
        Published = 2,
        /// <summary>
        /// 已结束
        /// </summary>
        [Description("已结束")]
        Over = 3,
        /// <summary>
        /// 已到期
        /// </summary>
        [Description("已到期")]
        Expired = 4

    }
}
