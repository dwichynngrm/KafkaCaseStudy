using ShoppingAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using System;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ShoppingAPI.Kafka;


namespace ShoppingAPI.GraphQL
{
    public class Mutation
    {
        public async Task<TransactionStatus> AddProductAsync(
            ProductInput input,
            [Service] IOptions<KafkaSettings> kafkaSettings)
        {
            var product = new Product
            {
                Name = input.Name,
                Stock = input.Stock,
                Price = input.Price,
                Created = DateTime.Now
            };
            var key = "product-add-" + DateTime.Now.ToString();
            var val = JObject.FromObject(product).ToString(Formatting.None);
            var result = await KafkaHelper.SendMessage(kafkaSettings.Value, "product", key, val);
            await KafkaHelper.SendMessage(kafkaSettings.Value, "logging", key, val);

            var ret = new TransactionStatus(result, "");
            if (!result)
                ret = new TransactionStatus(result, "Failed to submit data");


            return await Task.FromResult(ret);
        }
        public async Task<TransactionStatus> AddCartAsync(
            CartInput input,
            [Service] IOptions<KafkaSettings> kafkaSettings)
        {
            var cart = new Cart
            {
                Username = input.Username,
                ProductId = input.ProductId,
                Quantity = input.Quantity
            };
            var key = "cart-add-" + DateTime.Now.ToString();
            var val = JObject.FromObject(cart).ToString(Formatting.None);
            var result = await KafkaHelper.SendMessage(kafkaSettings.Value, "cart", key, val);
            await KafkaHelper.SendMessage(kafkaSettings.Value, "logging", key, val);

            var ret = new TransactionStatus(result, "");
            if (!result)
                ret = new TransactionStatus(result, "Failed to submit data");


            return await Task.FromResult(ret);
        }
    }
}
