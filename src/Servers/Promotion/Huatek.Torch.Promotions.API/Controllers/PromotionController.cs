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
using Microsoft.AspNetCore.JsonPatch;
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
        private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

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

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Summaries;
        }

        /// <summary>
        /// 获取活动信息
        /// </summary>
        /// <remarks>
        /// </remarks>
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

            var totalItems = await _promotionContext.Promotions
                .LongCountAsync();

            var itemsOnPage = await _promotionContext.Promotions
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

            var items = await _promotionContext.Promotions.Where(ci => idsToSelect.Contains(ci.Id)).ToListAsync();


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

            var item = await _promotionContext.Promotions.SingleOrDefaultAsync(ci => ci.Id == id);


            if (item != null)
            {
                return item;
            }

            return NotFound();
        }

      

        /// <summary>
        /// 更新活动信息
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
        /// 创建活动 此接口可以活动和活动商品一起插入到数据库
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
                return BadRequest("Create Failed");
            }
            _logger.LogInformation("CreateProductAsync Success {@result}.", result);
            return CreatedAtAction(nameof(ItemByIdAsync), new { id = result.Id }, null);
            //}

        }


        /// <summary>
        /// 此方法是物理删除,删除活动请使用PATCH的方法,逻辑删除
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        //[Route("{id}")]
        //[HttpDelete]
        //[Obsolete]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult> DeletePromotionAsync(int id)
        //{
        //    var result = await _promotionService.DeleteByIdAsync(id);
        //    if (result)
        //    {
        //        return NoContent();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }           
        //}





        // https://www.cnblogs.com/cgzl/p/9080960.html
        /// <summary>
        /// 修改删除状态 或 活动状态(已创建,已发布,已结束,已到期)
        /// </summary>
        /// <remarks>
        /// [ { "value":1, "operationType": 0, "path": "/Deleted", "op": "replace", "from": "" } ]
        /// [ { "value":2, "operationType": 0, "path": "/PromotionStateId", "op": "replace", "from": "" } ]
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> PatchPromotionAsync(int id, [FromBody] JsonPatchDocument<Promotion> patch)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            try
            {
                patch.ApplyTo(promotion);
                await _promotionService.UpdatePromotionAsync(promotion);

                _logger.LogInformation("PatchPromotionAsync Success {@promotion}.", promotion);
            }
            catch (Exception)
            {
                _logger.LogError("PatchPromotionAsync Failed {@patch}---{@promotion}", patch, promotion);
                return StatusCode(500, "Paching Failed ");
            }
          

            return CreatedAtAction(nameof(ItemByIdAsync), new { id = promotion.Id }, null);

        }

        /// <summary>
        /// 根据活动id获取活动商品
        /// </summary>
        /// <param name="promotionId">活动id</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/items/{promotionId:int?}")]
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
        /// 给活动添加商品
        /// </summary>
        /// <param name="promotionProducts">活动商品信息</param>
        /// <returns></returns>
        [Route("product")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> CreateProductPromotionsAsync([FromBody] IEnumerable<PromotionProduct> promotionProducts)
        {
            if (promotionProducts == null || !promotionProducts.Any())
            {
                return BadRequest("Add Failed,Not Data !");
            }
            var promotionId = promotionProducts.FirstOrDefault().PromotionId;
            var promotion = await _promotionService.GetPromotionByIdAsync(promotionId);
            if (promotion == null)
            {
                return NotFound(new { Message = $"Item with promotionId {promotionId} not found." });
                // return BadRequest("Add Failed,promotionId Is Invalid !");
            }
            foreach (var promotionProduct in promotionProducts)
            {
                promotion.PromotionProducts.Add(promotionProduct);
            }
            await _promotionService.UpdatePromotionAsync(promotion);
            _logger.LogInformation("CreateProductPromotionsAsync Success {@promotion}.", promotion);
            return CreatedAtAction(nameof(ItemByIdAsync), new { id = promotion.Id }, null);
        }
        /// <summary>
        /// 根据id获取活动商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("product/items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(PromotionProduct), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PromotionProduct>> PromotionProductByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _promotionContext.PromotionProducts.SingleOrDefaultAsync(ci => ci.Id == id);


            if (item != null)
            {
                return item;
            }

            return NotFound();
        }

        /// <summary>
        /// 修改活动商品的库存货活动价
        /// </summary>
        /// <remarks>
        /// [ { "value":88, "operationType": 0, "path": "/StockQuantity", "op": "replace", "from": "" } ]
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [Route("product/{id}")]
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> PatchPromotionProductAsync(int id, [FromBody] JsonPatchDocument<PromotionProduct> patch)
        {

            var promotionProduct = await _promotionContext.FindAsync<PromotionProduct>(id);
            if (promotionProduct == null)
            {
                return NotFound();
            }
           
            try
            {
                patch.ApplyTo(promotionProduct);
                _promotionContext.Update(promotionProduct);
                await _promotionContext.SaveChangesAsync();

                _logger.LogInformation("PatchPromotionProductAsync Success {@promotionProduct}.", promotionProduct);
            }
            catch (Exception)
            {
                _logger.LogError("PatchPromotionProductAsync Failed {@patch}---{@promotionProduct}", patch, promotionProduct);
                return StatusCode(500, "Paching Failed ");
            }


            return CreatedAtAction(nameof(ItemByIdAsync), new { id = promotionProduct.Id }, null);

        }
        /// <summary>
        /// 删除活动商品
        /// </summary>
        /// <param name="id">PromotionProductId</param>
        /// <returns></returns>
        [Route("product/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeletePromotionProductAsync(int id)
        {
            var promotionProduct = await _promotionContext.FindAsync<PromotionProduct>(id);
            if (promotionProduct == null)
            {
                return NotFound();
            }

            _promotionContext.Remove(promotionProduct);
            await _promotionContext.SaveChangesAsync();
            _logger.LogInformation("DeletePromotionProductAsync Success {@promotionProduct}.", promotionProduct);

            return NoContent();
        }

        /// <summary>
        /// 根据商品获取它所参加的活动
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("promotionproduct/items/{productId:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<Promotion>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Promotion>>> PromotionByProductIdAsync(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest();
            }

            var promotionIds = _promotionContext.PromotionProducts.Where(p => p.ProductId == productId).Select(p=>p.PromotionId);
            if (promotionIds == null || !promotionIds.Any())
            {
                return NotFound();
            }

            var items = await GetItemsByIdsAsync(string.Join(",", promotionIds));

            if (!items.Any())
            {
                return NotFound();
            }

            return Ok(items);
        }
    }
}
