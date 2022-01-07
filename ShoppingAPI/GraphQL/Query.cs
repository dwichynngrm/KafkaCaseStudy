using System;
using ShoppingAPI.Models;
using System.Linq;
using HotChocolate;
using ShoppingAPI.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace ShoppingAPI.GraphQL
{
    public class Query
    {
        public async Task<IQueryable<Product>> GetProducts(
            [Service] ShopDbContext context,
            [Service] IOptions<KafkaSettings> kafkaSettings)
        {
            var key = "GetProducts-" + DateTime.Now.ToString();
            var val = JObject.FromObject(new { Message = "GraphQL Query GetProducts" }).ToString(Formatting.None);

            await KafkaHelper.SendMessage(kafkaSettings.Value, "logging", key, val);
            return context.Products;
        }

        public IQueryable<Product> GetProductById(
            [Service] ShopDbContext context,
            int id,
            [Service] IOptions<KafkaSettings> kafkaSettings)
        {
            var key = "GetProductById-" + DateTime.Now.ToString();
            var val = JObject.FromObject(new { Message = "GraphQL Query GetProductById" }).ToString(Formatting.None);

            _ = KafkaHelper.SendMessage(kafkaSettings.Value, "logging", key, val);

            var products = context.Products.Where(p => p.Id == id);

            return products;
        }
        public IQueryable<Cart> GetCarts(
            [Service] ShopDbContext context,
            [Service] IOptions<KafkaSettings> kafkaSettings)
        {
            var key = "GetCarts-" + DateTime.Now.ToString();
            var val = JObject.FromObject(new { Message = "GraphQL Query GetCarts" }).ToString(Formatting.None);

            _ = KafkaHelper.SendMessage(kafkaSettings.Value, "logging", key, val);

            return context.Carts;
        }

        public IQueryable<Cart> GetCartByUsername(
            [Service] ShopDbContext context,
            string username,
            [Service] IOptions<KafkaSettings> kafkaSettings)
        {
            var key = "GetCartByUsername-" + DateTime.Now.ToString();
            var val = JObject.FromObject(new { Message = "GraphQL Query GetCartByUsername" }).ToString(Formatting.None);

            _ = KafkaHelper.SendMessage(kafkaSettings.Value, "logging", key, val);

            var carts = context.Carts.Where(p => p.Username == username);

            return carts;
        }
    }
}
