using DotNetCore.CAP;
using Huatek.Torch.Infrastructure.Core.Behaviors;
using Huatek.Torch.Promotion.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huatek.Torch.Promotion.Infrastructure
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
