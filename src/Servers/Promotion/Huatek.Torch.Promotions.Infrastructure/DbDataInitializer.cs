using Huatek.Torch.Promotions.Domain.Enum;
using Huatek.Torch.Promotions.Domain.PromotionAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Huatek.Torch.Promotions.Infrastructure
{
    public class DbDataInitializer
    {
        public static void Initialize(PromotionContext context)
        {
            //如果没有数据库，那么新建数据库
            //context.Database.EnsureCreated();
            //context.Database.Migrate();
            if (context.Promotions.Any())
            {
                return;
            }
            context.Promotions.AddRange(Initialize());
            if (context.SaveChanges() == 0)
            {
                throw new Exception("写入默认数据失败。");
            }
        }


        /// <summary>
        /// 种子数据
        /// </summary>
        /// <returns></returns>
        private static List<Promotion> Initialize()
        {

            var promotions = new List<Promotion>()
            {
                new Promotion()
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
                            PromotionId=1,
                            ProductId =6,
                            Price =13,
                            StockQuantity = 111111,
                            Deleted =false
                        }, new PromotionProduct()
                        {
                            PromotionId=1,
                            ProductId =5,
                            Price =14,
                            StockQuantity = 11111,
                            Deleted =false
                        }, new PromotionProduct()
                        {
                            PromotionId=1,
                            ProductId =4,
                            Price =15,
                            StockQuantity = 1111,
                            Deleted =false
                        }, new PromotionProduct()
                        {
                            PromotionId=1,
                            ProductId =3,
                            Price =16,
                            StockQuantity = 111,
                            Deleted =false
                        }, new PromotionProduct()
                        {
                            PromotionId=1,
                            ProductId =2,
                            Price =17,
                            StockQuantity = 11,
                            Deleted =false
                        }
                    }
                },new Promotion()
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
                },new Promotion()
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
                }

            };

            return promotions;
        }
    }
}
