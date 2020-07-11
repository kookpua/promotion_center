using Autofac;
using Huatek.Torch.Promotions.Service;

namespace Huatek.Torch.Promotions.APP.Extensions
{
    public class PromotionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PromotionService>().As<IPromotionService>();
        }
    }
}
