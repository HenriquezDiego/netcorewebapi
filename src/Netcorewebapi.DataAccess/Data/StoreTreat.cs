using Netcorewebapi.DataAccess.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Netcorewebapi.DataAccess.Data.Entities;

namespace Netcorewebapi.DataAccess.Data
{
    public class StoreTreat
    {
        private ApplicationDbContext Context { get; }
        private IHostingEnvironment Hosting { get; }

        public StoreTreat(ApplicationDbContext context, IHostingEnvironment hosting)
        {
            Context = context;
            Hosting = hosting;
        }

        public void Seed()
        {

            Context.Database.EnsureCreated();
            if (Context.Products.Any()) return;
            var filepath = Path.Combine(Hosting.ContentRootPath, @"..\Netcorewebapi.DataAccess\Data\art.json");
            var json = File.ReadAllText(filepath);
            //JObject json = JObject.Parse(File.ReadAllText(filepath));
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
            var enumerable = products.ToList();
            Context.Products.AddRange(enumerable);

            var order = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = "123456",
                Items = new List<OrderItem>() {

                    new OrderItem(){

                        Product= enumerable.First(),
                        Quantity = 5,
                        UnitPrice = enumerable.First().Price

                    },

                    new OrderItem(){

                        Product= enumerable.Last(),
                        Quantity = 80,
                        UnitPrice = enumerable.First().Price

                    }
                }
            };

                
            Context.Order.Add(order);
            Context.SaveChanges();

        }

    }
}
       

