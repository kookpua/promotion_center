using Autofac;
using Huatek.Torch.Promotion.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotion.API.Extensions
{
    public class PromotionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PromotionService>().As<IPromotionService>();
        }
    }
}
