using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Domain.Enum
{
    /// <summary>
    /// 活动商品类型
    /// </summary>
    public enum PromotionProductType
    {
        /// <summary>
        /// 可设置多个商品
        /// </summary>
        [Description("可设置多个商品")]
        SelectMany =1,
        /// <summary>
        /// 只能设置一个商品
        /// </summary>
        [Description("只能设置一个商品")]
        OnlyOne =2,
        /// <summary>
        /// 全部商品
        /// </summary
        [Description("全部商品")]
        All =3
    }
}
