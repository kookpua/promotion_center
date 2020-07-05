using System;
using System.Collections.Generic;
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
        Created = 1,
        /// <summary>
        /// 已发布
        /// </summary>
        Published = 2,
        /// <summary>
        /// 已结束
        /// </summary>
        Over = 3,
        /// <summary>
        /// 已到期
        /// </summary>
        Expired = 4

    }
}
