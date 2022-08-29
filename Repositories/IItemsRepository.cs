﻿using System;
using System.Collections.Generic;
using WSEIbackendREST.Entities;

namespace WSEIbackendREST.Repositories
{
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}