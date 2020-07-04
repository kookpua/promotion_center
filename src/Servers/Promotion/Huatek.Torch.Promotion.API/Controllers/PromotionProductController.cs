using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Promotion.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionProductController : ControllerBase
    {
      

        //private readonly ILogger<PromotionController> _logger;
        //private readonly IPromotionService _promotionService;

        //public PromotionProductController(
        //    ILogger<PromotionController> logger,
        //    IPromotionService promotionService)
        //{
        //    _logger = logger;
        //    _promotionService = promotionService;
        //}

        //[HttpGet]
        //public IEnumerable<PromotionProduct> Get()
        //{
        //    _logger.LogInformation("Hello, Serilog!");
        //    _logger.LogInformation("你好, 西安!");

        //    return new List<PromotionProduct>();
        //}
    }
}
