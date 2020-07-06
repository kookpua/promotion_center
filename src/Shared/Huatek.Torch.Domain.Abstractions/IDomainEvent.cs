using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
namespace Huatek.Torch.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
    }
}
