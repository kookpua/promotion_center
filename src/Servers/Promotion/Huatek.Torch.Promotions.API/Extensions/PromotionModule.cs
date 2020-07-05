using Autofac;
using Huatek.Torch.Promotions.Infrastructure;
using Huatek.Torch.Promotions.Infrastructure.Repositories;
using Huatek.Torch.Promotions.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.API.Extensions
{
    public class PromotionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PromotionRepository>().As<IPromotionRepository>();
            builder.RegisterType<PromotionService>().As<IPromotionService>();
        }
    }
}
