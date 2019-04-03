using csharp_lazy_load_property.Data;
using csharp_lazy_load_property.Models;
using csharp_lazy_load_property.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
                    Console.WriteLine("Initial pull of model.");
                    Console.WriteLine($"Id: {city.Id} JsonData: {city.JsonData} JsonDataDictionary is null? {city._JsonDataDictionary == null}");

                    Console.WriteLine("Pull value from dictionary - triggers lazy loading.");
                    Console.WriteLine($"Id: {city.Id} City: {city.JsonDataDictionary["Name"]} Population: {city.JsonDataDictionary["Population"]}");

                    Console.WriteLine("Re-check model.");
                    Console.WriteLine($"Id: {city.Id} JsonData: {city.JsonData} JsonDataDictionary is null? {city._JsonDataDictionary == null}");

                    Console.WriteLine("Change the JsonData value.");
                    switch (city.JsonDataDictionary["Name"])
                    {
                        case "Indianapolis":
                            city.JsonData = "{\"Name\": \"Indianapolis\", \"Population\": 2222}";
                            break;
                        case "Chicago":
                            city.JsonData = "{\"Name\": \"Chicago\", \"Population\": 8888}";
                            break;
                        default:
                            break;
                    }

                    Console.WriteLine("Re-pull value from dictionary - updates because JsonData changed.");
                    Console.WriteLine($"Id: {city.Id} City: {city.JsonDataDictionary["Name"]} Population: {city.JsonDataDictionary["Population"]}");

                    Console.WriteLine("");
                }
            }
        }

        private static void AddTestData(DataContext context)
        {
            var city1 = new City
            {
                Id = 1,
                JsonData = "{\"Name\": \"Indianapolis\", \"Population\": 1111}"
            };

            context.Cities.Add(city1);

            var city2 = new City
            {
                Id = 2,
                JsonData = "{\"Name\": \"Chicago\", \"Population\": 9999}"
            };

            context.Cities.Add(city2);

            context.SaveChanges();
        }
    }
}
