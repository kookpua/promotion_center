using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotion.Domain.Enum
{
    /// <summary>
    /// 活动商品类型
    /// </summary>
    public enum PromotionProductType
    {
        /// <summary>
        /// 可设置多个商品
        /// </summary>
        SelectMany=1,
        /// <summary>
        /// 只能设置一个商品
        /// </summary>
        OnlyOne=2,
        /// <summary>
        /// 全部商品
        /// </summary>
        All=3
    }
}
