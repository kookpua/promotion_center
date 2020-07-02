using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Promotion.Data;
using Promotion.Domin;

namespace Promotion.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionController : ControllerBase
    {
      

        private readonly ILogger<PromotionController> _logger;
        private readonly PromotionContext _context;

        public PromotionController(ILogger<PromotionController> logger,PromotionContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Promotions> Get()
        {
            var promotions = _context.Promotions.ToList();
            return promotions;
        }
    }
}
