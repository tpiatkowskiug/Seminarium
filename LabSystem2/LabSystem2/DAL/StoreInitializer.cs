using Microsoft.AspNet.Identity.EntityFramework;
using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using LabSystem2.Migrations;
using System.Data.Entity.Migrations;

namespace LabSystem2.DAL
{
    public class StoreInitializer : MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>  //DropCreateDatabaseAlways<ApplicationDbContext>
    {
        //protected override void Seed(StoreContext context)
        //{
        //    InitializeIdentityForEF(context);
        //    SeedStoreData(context);

        //    base.Seed(context);
        //}

        public static void SeedStoreData(ApplicationDbContext context)
        {
            var genres = new List<Genre>
            {
                new Genre() { GenreId = 1, Name = "Badania Rolnicze", IconFilename = "rock.png" },
                new Genre() { GenreId = 2, Name = "Badania Ogrodnicze", IconFilename = "metal.png" },
                new Genre() { GenreId = 3, Name = "Badania Sadownicze", IconFilename = "jazz.png" },
                new Genre() { GenreId = 4, Name = "Badania Inne", IconFilename = "hiphop.png" },
                new Genre() { GenreId = 5, Name = "Nawozy Organiczne", IconFilename = "rnb.png" },
                new Genre() { GenreId = 6, Name = "Nawozy Mineralne", IconFilename = "pop.png" },
                new Genre() { GenreId = 7, Name = "Nawozy wapniowe", IconFilename = "reagge.png" },
                new Genre() { GenreId = 8, Name = "Promocje", IconFilename = "promos.png" }
            };

            genres.ForEach(g => context.Genres.AddOrUpdate(g));
            context.SaveChanges();

            var albums = new List<Product>
            {
                new Product() { ProductId = 1, ProductTitle = "ph, P2O5, K2O, Mg", PriceNetto = 13, PriceBrutto = 13, CoverFileName = "1.png", IsBestseller = true, DateAdded = new DateTime(2019, 02, 1), GenreId = 1 },
                new Product() { ProductId = 2, ProductTitle = "Cu, Fe, Mn, Zn, B", PriceNetto = 14, PriceBrutto = 50, CoverFileName = "2.png", IsBestseller = true, DateAdded = new DateTime(2019, 02, 1), GenreId = 1 },
                new Product() { ProductId = 3, ProductTitle = "Oznaczenie Corg.", PriceNetto = 15, PriceBrutto = 30, CoverFileName = "3.png", IsBestseller = true, DateAdded = new DateTime(2019, 02, 1), GenreId = 1 },
                new Product() { ProductId = 4, ProductTitle = "ph, P2O5, K2O, Mg", PriceNetto = 20, PriceBrutto = 40, CoverFileName = "4.png", IsBestseller = true, DateAdded = new DateTime(2016, 02, 1), GenreId = 2 },

            };

            albums.ForEach(a => context.Productus.AddOrUpdate(a));
            context.SaveChanges();

            var employee = new List<Employee>
            {
                new Employee() { EmployeeId = 1, NameAndSurname = "Jan Kowalski", Email = "jkowalski@oschr.pl", PhoneNumber = "609-257-047", Region = "Płońsk" },
                new Employee() { EmployeeId = 2, NameAndSurname = "Jan Nowak", Email = "jnowak@oschr.pl", PhoneNumber = "609-257-048", Region = "Warmia" },
                new Employee() { EmployeeId = 3, NameAndSurname = "Bogdan Smela",  Email = "bsmela@oschr.pl", PhoneNumber = "609-257-049", Region = "Kutno" },
                new Employee() { EmployeeId = 4, NameAndSurname = "Łukasz Załuski",  Email = "lzaluski@oschr.pl", PhoneNumber = "609-257-040", Region = "Żuromin" },

            };

            employee.ForEach(b => context.Employees.AddOrUpdate(b));
            context.SaveChanges();
        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            ////var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ////var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            ////const string name = "admin@spodigly.pl";
            ////const string password = "P@ssw0rd";
            ////const string roleName = "Admin";


            ////var user = userManager.FindByName(name);
            ////if (user == null)
            ////{
            ////    user = new ApplicationUser { UserName = name, Email = name, UserData = new UserData() };
            ////    var result = userManager.Create(user, password);
            ////    result = userManager.SetLockoutEnabled(user.Id, false);
            ////}

            //Create Role Admin if it does not exist
            ////var role = roleManager.FindByName(roleName);
            ////if (role == null)
            ////{
            ////    role = new IdentityRole(roleName);
            ////    var roleresult = roleManager.Create(role);
            ////}

            //var user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}

            // Add user admin to Role Admin if not already added
            ////var rolesForUser = userManager.GetRoles(user.Id);
            ////if (!rolesForUser.Contains(role.Name))
            ////{
            ////    var result = userManager.AddToRole(user.Id, role.Name);
            ////}
        }
    }
}