using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Huatek.Torch.Promotions.API.ViewModel;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Huatek.Torch.Promotions.Infrastructure;
using Huatek.Torch.Promotions.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Huatek.Torch.Promotions.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly ILogger<PromotionController> _logger;
        private readonly IPromotionService _promotionService;
        private readonly IMapper _mapper;
        private readonly PromotionContext _promotionContext;

        public PromotionController(PromotionContext context,
            ILogger<PromotionController> logger,
            IPromotionService promotionService,
            IMapper mapper
            )
        {
            _promotionContext = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _promotionService = promotionService;
            _mapper = mapper;
        }

        /// <summary>
        /// GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        /// 获取活动信息
        /// </summary>
        /// <param name="pageSize">每页的数量</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="ids">根据id list获取，逗号隔开</param>
        /// <returns></returns>
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Promotion>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Promotion>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 15,
            [FromQuery] int pageIndex = 0, string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = await GetItemsByIdsAsync(ids);

                if (!items.Any())
                {
                    return BadRequest("ids value invalid. Must be comma-separated list of numbers");
                }

                return Ok(items);
            }

            var totalItems = await _promotionContext.Promotion
                .LongCountAsync();

            var itemsOnPage = await _promotionContext.Promotion
                .OrderBy(c => c.Id)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();



            var model = new PaginatedItemsViewModel<Promotion>(pageIndex, pageSize, 
                totalItems, itemsOnPage);

            return Ok(model);
        }
        private async Task<List<Promotion>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<Promotion>();
            }

            var idsToSelect = numIds
                .Select(id => id.Value);

            var items = await _promotionContext.Promotion.Where(ci => idsToSelect.Contains(ci.Id)).ToListAsync();


            return items;
        }

        /// <summary>
        /// 根据id获取活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Promotion), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Promotion>> ItemByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _promotionContext.Promotion.SingleOrDefaultAsync(ci => ci.Id == id);


            if (item != null)
            {
                return item;
            }

            return NotFound();
        }

        /// <summary>
        /// 根据活动id获取活动商品api/v1/[controller]/items/product/[?pageSize=3&pageIndex=10]
        /// </summary>
        /// <param name="promotionId">活动id</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("items/product/{promotionId:int?}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<PromotionProduct>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<PromotionProduct>>>
            ItemsByPromotionIdAsync(int? promotionId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<PromotionProduct>)_promotionContext.PromotionProducts;

            if (promotionId.HasValue)
            {
                root = root.Where(ci => ci.PromotionId == promotionId);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();


            return new PaginatedItemsViewModel<PromotionProduct>(pageIndex, pageSize, totalItems, itemsOnPage);
        }


        /// <summary>
        /// 更新活动信息 api/v1/[controller]/items
        /// </summary>
        /// <param name="promotionToUpdate"></param>
        /// <returns></returns>
        [Route("items")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateProductAsync([FromBody] Promotion promotionToUpdate)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(promotionToUpdate.Id);
            if (promotion == null)
            {
                return NotFound(new { Message = $"Item with id {promotionToUpdate.Id} not found." });
            }


            // Update current promotion
            promotion = promotionToUpdate;
            var result = await _promotionService.UpdatePromotionAsync(promotion);


            return CreatedAtAction(nameof(ItemByIdAsync), new { id = promotionToUpdate.Id }, null);
        }


        /// <summary>
        /// 创建活动 api/v1/[controller]/items
        /// </summary>
        /// <param name="promotion">活动信息</param>
        /// <returns></returns>
        [Route("items")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateProductAsync([FromBody] Promotion promotion)
        {
            //if (ModelState.IsValid)
            //{
                var result = await _promotionService.AddPromotionAsync(promotion);
                if (result.Id == 0)
                {
                    _logger.LogError("CreateProductAsync {@result}.", result);
                    return BadRequest("Create Fail");
                }
                _logger.LogInformation("CreateProductAsync Success {@result}.", result);
                return CreatedAtAction(nameof(ItemByIdAsync), new { id = result.Id }, null);
            //}

        }


        /// <summary>
        /// 删除活动 api/v1/[controller]/id
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeletePromotionAsync(int id)
        {
            var result = await _promotionService.DeleteByIdAsync(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
           
        }
    }
}
