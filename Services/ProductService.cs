using API_CRUD_MongoDB.DTO;
using API_CRUD_MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API_CRUD_MongoDB.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Products> _products;

        public ProductService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _products = mongoDatabase.GetCollection<Products>(databaseSettings.Value.CollectionName);
        }

        public async Task<List<Products>> GetProducts()
        {
            return await _products.Find(product => true).ToListAsync();
        }

        public async Task<Products> GetProductById(string id)
        {
            return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Products> CreateProduct(Products products)
        {
            await _products.InsertOneAsync(products);
            return products;
        }

        public async Task UpdateProduct(string id, ProductDTO productDTO)
        {
            var product_update = Builders<Products>.Update.Set(p => p.Name, productDTO.Name)
                                                          .Set(p => p.Price, productDTO.Price)
                                                          .Set(p => p.Stock, productDTO.Stock);

            await _products.UpdateOneAsync(p => p.Id == id, product_update);
        }

        public async Task DeleteProduct(string id)
        {
            await _products.DeleteOneAsync(p => p.Id == id);
        }
    }
}
