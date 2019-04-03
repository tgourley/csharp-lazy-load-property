using csharp_lazy_load_property.Data;
using csharp_lazy_load_property.Models;
using csharp_lazy_load_property.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace csharp_lazy_load_property
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptions<DataContext> options;
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase();
            options = builder.Options;

            using (var db = new DataContext(options))
            {
                AddTestData(db);

                var cityService = new CityService(db);
                
                foreach (var city in cityService.Find())
                {
                    Console.WriteLine($"Id: {city.Id} JsonData: {city.JsonData}");
                }
            }
        }

        private static void AddTestData(DataContext context)
        {
            var city1 = new City
            {
                Id = 1,
                JsonData = "{\"Name\": \"Indianapolis\", \"Population\": 123456}"
            };

            context.Cities.Add(city1);

            var city2 = new City
            {
                Id = 2,
                JsonData = "{\"Name\": \"Chicago\", \"Population\": 987654}"
            };

            context.Cities.Add(city2);

            context.SaveChanges();
        }
    }
}
