using AutoMapper;
using Huatek.Torch.Domain;
using Huatek.Torch.Promotions.Domain.Enum;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch.Promotions.Profiles
{
    public class PromotionProfile : Profile
    {
        public PromotionProfile()
        {
            CreateMap<Promotion, PromotionDto>().
                ForMember(dest => dest.PromotionTitle,
                opt => opt.MapFrom(src => src.Title));
            
            //use:
            //var promotiondto = _mapper.Map<IEnumerable<PromotionDto>>(itemsOnPage);

            CreateMap<PromotionProduct, PromotionProductDto>();
        }
    }
}