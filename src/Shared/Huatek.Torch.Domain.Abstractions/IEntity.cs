﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Huatek.Torch.Domain.Abstractions
{
    public interface IEntity
    {
        object[] GetKeys();
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}
