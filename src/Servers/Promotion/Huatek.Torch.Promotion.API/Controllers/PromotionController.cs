using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using Huatek.Torch.Promotion.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Promotion.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly ILogger<PromotionController> _logger;
        private readonly IPromotionService _promotionService;

        public PromotionController(
            ILogger<PromotionController> logger,
            IPromotionService promotionService)
        {
            _logger = logger;
            _promotionService = promotionService;
        }

        [HttpGet]
        public IEnumerable<Promotions> Get()
        {

            return _promotionService.GetAllPromotions();
        }
    }
}
