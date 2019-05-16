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
                new Genre() { GenreId = 1, Name = "Badania Rolnicze", IconFilename = "beaker.png" },
                new Genre() { GenreId = 2, Name = "Badania Ogrodnicze", IconFilename = "flask.png" },
                new Genre() { GenreId = 3, Name = "Badania Sadownicze", IconFilename = "laboratory.png" },
                new Genre() { GenreId = 4, Name = "Badania Inne", IconFilename = "atom.png" },
                new Genre() { GenreId = 5, Name = "Nawozy Organiczne", IconFilename = "pipette.png" },
                new Genre() { GenreId = 6, Name = "Nawozy Mineralne", IconFilename = "three-test-tubes.png" },
                new Genre() { GenreId = 7, Name = "Nawozy wapniowe", IconFilename = "experimentation.png" },
                new Genre() { GenreId = 8, Name = "Promocje", IconFilename = "loupe.png" }
            };

            genres.ForEach(g => context.Genres.AddOrUpdate(g));
            context.SaveChanges();

            var albums = new List<Product>
            {
                new Product() { ProductId = 1, ProductTitle = "ph, P2O5, K2O, Mg", PriceNetto = 13, PriceBrutto = 13, CoverFileName = "2.jpg", IsBestseller = true, DateAdded = new DateTime(2019, 02, 1), GenreId = 1, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam molestie condimentum facilisis. Vestibulum blandit turpis ac augue ornare, facilisis fermentum risus sagittis. Sed eleifend sagittis massa, et eleifend magna hendrerit et. Aliquam id vulputate libero, sit amet elementum ante. Sed mi est, hendrerit sit amet mollis vitae, viverra nec nunc. Maecenas elementum consequat dui consectetur consectetur. Aliquam cursus dui eu lectus consectetur feugiat eget ultrices arcu. Pellentesque eget justo ullamcorper, lobortis diam eget, vestibulum urna. Integer feugiat volutpat sodales. Duis ac nisi elit." },
                new Product() { ProductId = 2, ProductTitle = "Cu, Fe, Mn, Zn, B", PriceNetto = 14, PriceBrutto = 50, CoverFileName = "3.jpg", IsBestseller = true, DateAdded = new DateTime(2019, 02, 1), GenreId = 1, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam molestie condimentum facilisis. Vestibulum blandit turpis ac augue ornare, facilisis fermentum risus sagittis. Sed eleifend sagittis massa, et eleifend magna hendrerit et. Aliquam id vulputate libero, sit amet elementum ante. Sed mi est, hendrerit sit amet mollis vitae, viverra nec nunc. Maecenas elementum consequat dui consectetur consectetur. Aliquam cursus dui eu lectus consectetur feugiat eget ultrices arcu. Pellentesque eget justo ullamcorper, lobortis diam eget, vestibulum urna. Integer feugiat volutpat sodales. Duis ac nisi elit."  },
                new Product() { ProductId = 3, ProductTitle = "Oznaczenie Corg.", PriceNetto = 15, PriceBrutto = 30, CoverFileName = "1.jpg", IsBestseller = true, DateAdded = new DateTime(2019, 02, 1), GenreId = 1, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam molestie condimentum facilisis. Vestibulum blandit turpis ac augue ornare, facilisis fermentum risus sagittis. Sed eleifend sagittis massa, et eleifend magna hendrerit et. Aliquam id vulputate libero, sit amet elementum ante. Sed mi est, hendrerit sit amet mollis vitae, viverra nec nunc. Maecenas elementum consequat dui consectetur consectetur. Aliquam cursus dui eu lectus consectetur feugiat eget ultrices arcu. Pellentesque eget justo ullamcorper, lobortis diam eget, vestibulum urna. Integer feugiat volutpat sodales. Duis ac nisi elit."  },
                new Product() { ProductId = 4, ProductTitle = "ph, P2O5, K2O, Mg", PriceNetto = 20, PriceBrutto = 40, CoverFileName = "5.jpg", IsBestseller = true, DateAdded = new DateTime(2016, 02, 1), GenreId = 2,Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam molestie condimentum facilisis. Vestibulum blandit turpis ac augue ornare, facilisis fermentum risus sagittis. Sed eleifend sagittis massa, et eleifend magna hendrerit et. Aliquam id vulputate libero, sit amet elementum ante. Sed mi est, hendrerit sit amet mollis vitae, viverra nec nunc. Maecenas elementum consequat dui consectetur consectetur. Aliquam cursus dui eu lectus consectetur feugiat eget ultrices arcu. Pellentesque eget justo ullamcorper, lobortis diam eget, vestibulum urna. Integer feugiat volutpat sodales. Duis ac nisi elit."  },

            };

            albums.ForEach(a => context.Productus.AddOrUpdate(a));
            context.SaveChanges();


        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@oschr.pl";
            const string password = "P@ssw0rd";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            //var user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}

            //Add user admin to Role Admin if not already added
            //var rolesForUser = userManager.GetRoles(user.Id);
            //if (!rolesForUser.Contains(role.Name))
            //{
            //    var result = userManager.AddToRole(user.Id, role.Name);
            //}
        }
    }
}