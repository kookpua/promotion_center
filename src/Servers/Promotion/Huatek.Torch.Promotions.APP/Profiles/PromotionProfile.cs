using AutoMapper;
using Huatek.Torch.Promotions.APP.ViewModel;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;

namespace Huatek.Torch.Promotions.Profiles
{
    public class PromotionProfile : Profile
    {
        public PromotionProfile()
        {
            //CreateMap<Promotion, PromotionDto>().
            //    ForMember(dest => dest.PromotionTitle,
            //    opt => opt.MapFrom(src => src.Title));

            //use:
            //var promotiondto = _mapper.Map<IEnumerable<PromotionDto>>(itemsOnPage);

            CreateMap<Promotion, PromotionDto>();


            CreateMap<PromotionDto, Promotion>();
        }
    }
}