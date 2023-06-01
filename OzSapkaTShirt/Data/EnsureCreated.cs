using OzSapkaTShirt.Data;
using OzSapkaTShirt.Models;

namespace OzSapkaTShirt2.Data
{
    public class EnsureCreated
    {
        private readonly ApplicationContext _context;
        public EnsureCreated(ApplicationContext context)
        {
            _context = context;
            createGender();
            createCities();
            
        }
        public void createGender()
        {
            if (!_context.Genders.Any())
            {
                var Genders = new List<Gender>
            {
                new Gender { Id=0,Name = "Belirtilmemiş" },
                new Gender { Id=1,Name = "Erkek"},
                new Gender { Id=2,Name = "Kadın"}
            };
                _context.Genders.AddRange(Genders);
                _context.SaveChanges();
            }
        }
        

        public void createCities()
        {
            if (!_context.Cities.Any())
            {
                var cities = new List<City>
            {
                new City { PlateCode=34,Name = "İstanbul" },
                new City { PlateCode=52,Name = "Ordu" },
                new City { PlateCode=35,Name = "İzmir" },
                new City { PlateCode=59,Name = "Tekirdağ" },
                new City { PlateCode=57,Name = "Sinop" },
                new City { PlateCode=22,Name = "Edirne" },
                new City { PlateCode=28,Name = "Giresun" },
                new City { PlateCode=58,Name = "Sivas" },
                new City { PlateCode=19,Name = "Çorum" },
                new City { PlateCode=06,Name = "Ankara" },
                new City { PlateCode=17,Name = "Çanakkale" },
                new City { PlateCode=10,Name = "Balıkesir" },
            };
                _context.Cities.AddRange(cities);
                _context.SaveChanges();
            }
        }
    }
}
