﻿using Catalog.Model.Entities;
using MongoDB.Driver;

namespace Catalog.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
