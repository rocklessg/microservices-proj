using Catalog.Data.Interfaces;
using Catalog.Model.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
            var database = client.GetDatabase(configuration.GetSection("DatabaseSettings:DatabaseName").Value);

            Products = database.GetCollection<Product>(configuration.GetSection("DatabaseSettings:CollectionName").Value);
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
