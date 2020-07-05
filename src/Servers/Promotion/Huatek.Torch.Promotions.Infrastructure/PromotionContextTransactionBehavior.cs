using DotNetCore.CAP;
using Huatek.Torch.Infrastructure.Core.Behaviors;
using Huatek.Torch.Promotions.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huatek.Torch.Promotions.Infrastructure
{
    public class PromotionContextTransactionBehavior<TRequest, TResponse> : 
        TransactionBehavior<PromotionContext, TRequest, TResponse>
    {
        public PromotionContextTransactionBehavior(PromotionContext dbContext
            ,ILogger<PromotionContextTransactionBehavior<TRequest, TResponse>> logger) 
            : base(dbContext, logger)
        {
        }
    }
}
