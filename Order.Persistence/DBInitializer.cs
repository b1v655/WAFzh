using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
namespace Order.Persistence
{
    public class DBInitializer
    {
        private static Category[] _category;
        private static PizzaOrderContext _context;
        public static void Initialize(PizzaOrderContext context, UserManager<Employee> userManager)
        {
            _context = context;
            context.Database.EnsureCreated();
            if (!_context.Menu.Any())
            {
                SeedOrder();
                SeedCategory();
                SeedEmployee();
                SeedMenu();
            }

            
            if (userManager != null)
            {
                var adminUser = new Employee()
                {
                    UserName = "admin",
                    Name = "Kedves Admin"
                };
                var adminPassword = "Almafa123";

                _ = userManager.CreateAsync(adminUser, adminPassword).Result;
            }
            _context.SaveChanges();
        }
        private static void SeedMenu()
        {
            var menu = new Menu[]
            {
                new Menu
                {
                    Name = "Aspen pizza",
                    Description = "tárkonyos tejföl, bacon, hagyma, gomba, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                  

                },
                new Menu
                {
                    Name = "BBQ pizza",
                    Description="barbecue-szósz, bacon, csirkemell, hagyma, kaliforniai paprika, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Bivaly pizza",
                    Description="paradicsomszósz, chiliszósz, kolbász, zöldpaprika, jalapeno, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Bolognese pizza",
                    Description="bolognai ragu, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Carbonara pizza",
                    Description="fokhagymás tejföl, bacon, tojás, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Castello pizza",
                    Description="erõs-tejföl, bacon, tarja, kolbász, hagyma, jalapeno, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Chili Con Carne",
                    Description="bolognai ragu, bab, kukorica, jalapeno, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Cowboy menü pizza",
                    Description="paradicsomszósz, marha, bacon, vöröshagyma, paradicsomkarika, cheddar, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Dolce Vita pizza",
                    Description="paradicsomszósz, sonka, gomba, kukorica, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Escobar pizza",
                    Description="taco szósz, marha, bacon, kolbász, kaliforniai paprika, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Falusi pizza",
                    Description="tejföl, tarja,lilahagyma, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Fitness pizza",
                    Description="paradicsomszósz, kukorica, brokkoli, kaliforniai paprika, fetasajt, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },
                new Menu
                {
                    Name = "Genova pizza",
                    Description="tejföl, tarja, póréhagyma, füstölt mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Gyros pizza",
                    Description="gyros-szósz, csirkemell, paradicsomkarika, uborka, lilahagyma, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Hawaii pizza",
                    Description="paradicsomszósz, sonka, ananász, kukorica, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Húsimádó pizza",
                    Description="paradicsomszósz, sonka, tarja, bacon, szalámi, csirkemell, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Magyaros pizza",
                    Description="paradicsomszósz, szalámi, bacon, hagyma, pepperoni, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
                new Menu
                {
                    Name = "Margherita pizza",
                    Description="paradicsomszósz, paradicsomkarika, oregano, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },
               new Menu
                {
                    Name = "Mediterrán pizza",
                    Description="paradicsomszósz, csirkemell, fetasajt, olivabogyó, oregano, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Mexikói pizza",
                    Description="taco szósz, bacon, bab, kukorica, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Milano pizza",
                    Description="sajtkrém, sonka, csirke, kukorica, kaliforniai paprika, lilahagyma, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Molares pizza",
                    Description="aco alap, marha, bab, cheddar sajt, jalapeno, mozzarella, tortilla chips",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Napolitana pizza",
                    Description="paradicsomszósz, sonka, szalámi, tonhal, olivabogyó, parmesan, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Négy évszak pizza",
                    Description="paradicsomszósz, sonka, gomba, kukorica, szalámi, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Négysajtos pizza",
                    Description="paradicsomszósz, márványsajt, parmesan, füstölt mozzarella, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Olartiko pizza",
                    Description="mustáros-tejföl alap, bacon, kolbász, póréhagyma, kukorica, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Olaszkolbászos pizza",
                    Description="paradicsomszósz, kolbász, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Pármai pizza",
                    Description="paradicsomszósz, pármai sonka, hagyma, kukorica, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Perugia pizza",
                    Description="tejföl, csirkemell, kukorica, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Pomodoro pizza",
                    Description="paradicsomszósz, sonka, paradicsomkarika, lilahagyma, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Pompei pizza",
                    Description="bolognai ragu, bacon, szalámi, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Rukkola pizza",
                    Description="paradicsomszósz, pármai sonka, rukkola, oregano, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Sajtimádó pizza",
                    Description="sajtkrém, bacon, csirkemell, füstölt mozzarella, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Salerno pizza",
                    Description="paradicsomszósz, szalámi, paradicsomkarika, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Santana pizza",
                    Description="taco szósz, csirke, bacon, bab, kukorica, olivabogyó, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Sonka-gomba pizza",
                    Description="paradicsomszósz, sonka, gomba, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Sonka-kukorica pizza",
                    Description="paradicsomszósz, sonka, kukorica, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Sonkás pizza",
                    Description="paradicsomszósz, sonka, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Szalámis pizza",
                    Description="paradicsomszósz, szalámi, pepperoni, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Tenger gyümölcse pizza",
                    Description="paradicsomszósz, tintahal, rák, kagyló, polip, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Tengerész pizza",
                    Description="paradicsomszósz, tintahal, rák, kagyló, polip, tonhal, olivabogyó, mozzarella",
                    Category=_category[0],
                    Price=2095,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Tonhalas pizza",
                    Description="paradicsomszósz, tonhal, hagyma, paradicsomkarika, olivabogyó, mozzarella",
                    Category=_category[0],
                    Price=2395,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Babgulyás",
                    Description="Babgulyás",
                    Category=_category[1],
                    Price=1000,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Gulyásleves",
                    Description="Gulyásleves",
                    Category=_category[1],
                    Price=1000,
                    Count=0,
                    Spicy=true,
                    Vegetarian=false
                },
               new Menu
                {
                    Name = "Fokhagymakrémleves",
                    Description="Fokhagymakrémleves pirított zsemlekockával, sajttal",
                    Category=_category[1],
                    Price=500,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },
               new Menu
                {
                    Name = "Húsleves májgaluskával",
                    Description="Húsleves májgaluskával",
                    Category=_category[1],
                    Price=600,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },new Menu
                {
                    Name = "Húsleves finommetélttel",
                    Description="Húsleves finommetélttel",
                    Category=_category[1],
                    Price=600,
                    Count=0,
                    Spicy=false,
                    Vegetarian=false
                },new Menu
                {
                    Name = "Házi rétes",
                    Description="Házi rétes túróval töltve",
                    Category=_category[2],
                    Price=450,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },new Menu
                {
                    Name = "Palacsinta hagyományos töltelékkel",
                    Description="ízes, kakaós, fahéjas, nutellás",
                    Category=_category[2],
                    Price=300,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },new Menu
                {
                    Name = "Somlói galuska",
                    Description="Somlói galuska",
                    Category=_category[2],
                    Price=600,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },new Menu
                {
                    Name = "Gesztenyepüré",
                    Description="Gesztenyepüré tejszínhabbal",
                    Category=_category[2],
                    Price=600,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },new Menu
                {
                    Name = "Gundel palacsinta diótöltelékkel",
                    Description="Gundel palacsinta",
                    Category=_category[2],
                    Price=600,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },new Menu
                {
                    Name = "Gesztenyés palacsinta",
                    Description="Gesztenyés palacsinta csoki öntettel, tejszínhabbal",
                    Category=_category[2],
                    Price=600,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                },new Menu
                {
                    Name = "Túrós palacsinta",
                    Description="Túrós palacsinta vaníliás öntettel",
                    Category=_category[2],
                    Price=600,
                    Count=0,
                    Spicy=false,
                    Vegetarian=true
                }
            };
            foreach (Menu m in menu)
            {
                _context.Menu.Add(m);
            }
            _context.SaveChanges();
        }
        private static void SeedOrder()
        {
            _context.SaveChanges();
        }
        private static void SeedCategory()
        {
            _category = new Category[]
            {
                new Category
                {
                    Name="Pizzák"
                },
                new Category
                {
                    Name="Levesek"
                },
                new Category
                {
                    Name="Desszertek"
                }

            };

            foreach (Category c in _category)
            {
                _context.Category.Add(c);
            }
            _context.SaveChanges();
        }
        private static void SeedEmployee()
        {
          
            _context.SaveChanges();
        }
    }
}
