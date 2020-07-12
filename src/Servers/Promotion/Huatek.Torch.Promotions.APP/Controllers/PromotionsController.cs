using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Huatek.Torch.Promotions.API.ViewModel;
using Huatek.Torch.Promotions.APP.Extensions;
using Huatek.Torch.Promotions.APP.Models;
using Huatek.Torch.Promotions.APP.Utils;
using Huatek.Torch.Promotions.APP.ViewModel;
using Huatek.Torch.Promotions.Domain.Enum;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Huatek.Torch.Promotions.Infrastructure;
using Huatek.Torch.Promotions.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Serilog;

namespace Huatek.Torch.Promotions.APP.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    //public class PromotionController : ControllerBase
    public class PromotionsController : Controller
    {
        private readonly ILogger<PromotionsController> _logger;
        private readonly IPromotionService _promotionService;
        private readonly IMapper _mapper;
        private readonly PromotionContext _promotionContext;

        public PromotionsController(PromotionContext context,
            ILogger<PromotionsController> logger,
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
        /// 测试网站部署是否ok
        /// </summary>
        /// <returns></returns>
        [Route("Test")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>() { "ok"} ;
        }
        public async Task<IActionResult> Index(int promotionTypeId,
            int promotionProductTypeId, int promotionStateId,
            string searchString)
        {
            var promotions = from m in _promotionContext.Promotions
                             select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                promotions = promotions.Where(s => s.Title.Contains(searchString));
            }
            if (promotionTypeId > 0)
            {
                promotions = promotions.Where(s => s.PromotionTypeId == promotionTypeId);
            }
            if (promotionProductTypeId > 0)
            {
                promotions = promotions.Where(s => s.PromotionProductTypeId == promotionProductTypeId);
            }
            if (promotionStateId > 0)
            {
                promotions = promotions.Where(s => s.PromotionStateId == promotionStateId);
            }
            var promotionList = await promotions.ToListAsync();

            var model = new PromotionOptionViewModel
            {
                PromotionDtos = _mapper.Map<IList<PromotionDto>>(promotionList).ToList(),
                PromotionTypes = GeneratePromotionTypes(),
                PromotionProductTypes = GeneratePromotionProductTypes(),
                PromotionStates = GeneratePromotionStates(),
                SearchString = searchString,
                PromotionTypeId = promotionTypeId,
                PromotionProductTypeId = promotionProductTypeId,
                PromotionStateId = promotionStateId,
            };

            
            return View(model);
            //return View(await promotions.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["PromotionTypes"] = GeneratePromotionTypes();
            //ViewData["PromotionStates"] = GeneratePromotionStates();
            ViewData["PromotionProductTypes"] = GeneratePromotionProductTypes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Title,Description,PromotionTypeId,StartDate, EndDate,PromotionProductTypeId")]PromotionDto promotionDto)
        {
            promotionDto.PromotionState = PromotionState.Created;
            var flag = false;
            if (ModelState.IsValid)
            {
                if (promotionDto.EndDate <= promotionDto.StartDate
                    || promotionDto.EndDate<=DateTime.Now)
                {
                    ModelState.AddModelError("EndDate","结束时间不能小于开始时间或当前系统时间");
                }
                else { 
                    flag = true;
                }
            }
            if (flag)
            {
                var promotion = _mapper.Map<Promotion>(promotionDto);
                _promotionContext.Add(promotion);
                await _promotionContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PromotionTypes"] = GeneratePromotionTypes();
            //ViewData["PromotionStates"] = GeneratePromotionStates();
            ViewData["PromotionProductTypes"] = GeneratePromotionProductTypes();
            return View(promotionDto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _promotionContext.Promotions
                .FirstOrDefaultAsync(m => m.Id == id);

            var promotionDto = _mapper.Map<PromotionDto>(promotion);
            if (promotionDto == null)
            {
                return NotFound();
            }

            ViewData["PromotionTypes"] = GeneratePromotionTypes();
            ViewData["PromotionStates"] = GeneratePromotionStates();
            ViewData["PromotionProductTypes"] = GeneratePromotionProductTypes();
            return View(promotionDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind("Id,Title,Description,PromotionTypeId,PromotionStateId,Deleted,StartDate, EndDate,PromotionProductTypeId")] PromotionDto promotionDto)
        {
            var flag = false;
            if (ModelState.IsValid)
            {
                if (promotionDto.EndDate <= promotionDto.StartDate
                    || promotionDto.EndDate <= DateTime.Now)
                {
                    ModelState.AddModelError("EndDate", "结束时间不能小于开始时间或当前系统时间");
                }
                else
                {
                    flag = true;
                }
            }
            if (flag)
            {
                var promotion = _mapper.Map<Promotion>(promotionDto);
                _promotionContext.Update(promotion);
                await _promotionContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PromotionTypes"] = GeneratePromotionTypes();
            ViewData["PromotionStates"] = GeneratePromotionStates();
            ViewData["PromotionProductTypes"] = GeneratePromotionProductTypes();
            return View(promotionDto);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _promotionContext.Promotions
                .FirstOrDefaultAsync(m => m.Id == id);

            var promotionDto = _mapper.Map<PromotionDto>(promotion);
            if (promotionDto == null)
            {
                return NotFound();
            }

            return View(promotionDto);
        }


        private static List<SelectListItem> GeneratePromotionTypes()
        {
            var selectListItems = new List<SelectListItem>();
            var promotionTypes = EnumUtil.GetValues<PromotionType>();
            foreach (var item in promotionTypes)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Text = item.GetDescription(),
                    Value = ((int)item).ToString()
                });
            }
            return selectListItems;
        }

        private static List<SelectListItem> GeneratePromotionStates()
        {
            var selectListItems = new List<SelectListItem>(); 
            var promotionStates = EnumUtil.GetValues<PromotionState>();
            foreach (var item in promotionStates)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Text = item.GetDescription(),
                    Value = ((int)item).ToString()
                });
            }
            return selectListItems;
        }
        private static List<SelectListItem> GeneratePromotionProductTypes()
        {
            var selectListItems = new List<SelectListItem>();
            var promotionProductTypes = EnumUtil.GetValues<PromotionProductType>();
            foreach (var item in promotionProductTypes)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Text = item.GetDescription(),
                    Value = ((int)item).ToString()
                });
            }
            return selectListItems;
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
