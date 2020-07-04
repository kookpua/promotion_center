using Huatek.Torch.Promotion.API.ViewModel;
using Huatek.Torch.Promotion.Domain.Enum;
using Huatek.Torch.Promotion.Domain.PromotionAggregate;
using Huatek.Torch.Promotion.Infrastructure;
using Huatek.Torch.Promotion.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Promotion.API.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Huatek.Torch.Promotion.XUnitTest.Application
{
    public class PromotionControllerTest
    {

        private readonly DbContextOptions<PromotionContext> _dbOptions;

        public PromotionControllerTest()
        {
            _dbOptions = new DbContextOptionsBuilder<PromotionContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;

            using (var dbContext = new PromotionContext(_dbOptions))
            {
                dbContext.AddRange(GetFakeCatalog());
                dbContext.SaveChanges();
            }
        }

        [Fact]
        public async Task Get_PromotionProduct_Items_ByPromotionId_Success()
        {
            //Arrange
            var pageSize = 2;
            var pageIndex = 0;
            var promotionId = 1;

            var promotionContext = new PromotionContext(_dbOptions);

            var promotionServiceMock = new Mock<IPromotionService>();

            //Act
            var promotionController = new PromotionController(promotionContext,promotionServiceMock.Object);
            var actionResult = await promotionController.ItemsByPromotionIdAsync(promotionId,pageSize, pageIndex);

            //Assert 
            Assert.IsType<ActionResult<PaginatedItemsViewModel<PromotionProduct>>>(actionResult);
            var page = Assert.IsAssignableFrom<PaginatedItemsViewModel<PromotionProduct>>(actionResult.Value);
            Assert.Equal(pageIndex, page.PageIndex);
            Assert.Equal(pageSize, page.PageSize);
        }
        private List<Promotions> GetFakeCatalog()
        {
            return new List<Promotions>()
            {
                new Promotions()
                {
                    Title = "活动1",
                    Description ="活动1的说明",
                    PromotionTypeId =(int)PromotionType.NewUser,
                    CreatedOnUtc = DateTime.Now,
                    CreatedCustomerId =1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(100),
                    PromotionStateId = (int)PromotionState.Created,
                    Deleted =false,
                    PromotionProductTypeId = (int)PromotionProductType.SelectMany,
                    PromotionProducts=new List<PromotionProduct>(){
                        new PromotionProduct()
                        {
                            PromotionId = 1,
                            ProductId =6,
                            Price =13,
                            StockQuantity = 111111,
                            Deleted =false
                        }, new PromotionProduct()
                        {
                            PromotionId = 1,
                            ProductId =5,
                            Price =14,
                            StockQuantity = 11111,
                            Deleted =false
                        }, new PromotionProduct()
                        {
                            PromotionId = 1,
                            ProductId =4,
                            Price =15,
                            StockQuantity = 1111,
                            Deleted =false
                        }, new PromotionProduct()
                        {
                            PromotionId = 1,
                            ProductId =3,
                            Price =16,
                            StockQuantity = 111,
                            Deleted =false
                        }, new PromotionProduct()
                        {
                            PromotionId = 1,
                            ProductId =2,
                            Price =17,
                            StockQuantity = 11,
                            Deleted =false
                        }
                    }
                },new Promotions()
                {
                    Title = "活动2",
                    Description ="活动2的说明",
                    PromotionTypeId =(int)PromotionType.LimitDiscount,
                    CreatedOnUtc = DateTime.Now,
                    CreatedCustomerId =1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(100),
                    PromotionStateId = (int)PromotionState.Created,
                    Deleted =false,
                    PromotionProductTypeId = (int)PromotionProductType.OnlyOne
                },new Promotions()
                {
                    Title = "活动3",
                    Description ="活动3的说明",
                    PromotionTypeId =(int)PromotionType.LimitDiscount,
                    CreatedOnUtc = DateTime.Now,
                    CreatedCustomerId =1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(100),
                    PromotionStateId = (int)PromotionState.Created,
                    Deleted =false,
                    PromotionProductTypeId = (int)PromotionProductType.All
                },

            };
        }
    }
}
