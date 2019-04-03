using csharp_lazy_load_property.Data;
using csharp_lazy_load_property.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_lazy_load_property.Services
{
    public class CityService
    {
        private DataContext _context;

        public CityService(DataContext context)
        {
            _context = context;
        }

        public void Add(string jsonData)
        {
            var city = new City { JsonData = jsonData };
            _context.Cities.Add(city);
            _context.SaveChanges();
        }

        public IEnumerable<City> Find()
        {
            return _context.Cities
                .OrderBy(b => b.Id)
                .ToList();
        }
    }
}
