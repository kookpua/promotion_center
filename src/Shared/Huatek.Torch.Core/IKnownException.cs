using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huatek.Torch
{
    public interface IKnownException
    {
        string Message { get; }

        int ErrorCode { get; }

        object[] ErrorData { get; }
    }
}
