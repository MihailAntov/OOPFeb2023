﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public interface ICompositeGiftOperations
    {
        void Add(GiftBase gift);
        void Remove(GiftBase gift);
    }
}
