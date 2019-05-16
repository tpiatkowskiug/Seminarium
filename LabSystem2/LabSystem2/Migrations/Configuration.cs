namespace LabSystem2.Migrations
{
    using LabSystem2.DAL;
    using LabSystem2.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<LabSystem2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LabSystem2.Models.ApplicationDbContext context)
        {
            StoreInitializer.SeedStoreData(context);
            StoreInitializer.InitializeIdentityForEF(context);


            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Employee" },
                new IdentityRole { Name = "Customer" },
                new IdentityRole { Name = "Lab" }
                );

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole("37a6f998-c6d9-494a-bd33-813225078ce7", "Admin");
            UserManager.AddToRole("0f89670c-a738-4283-858a-d7ac2919143b", "Employee"); //piatkowski
            UserManager.AddToRole("3ade4ae3-6aae-48c2-a295-2f783ebd19b3", "Employee"); //smela
            UserManager.AddToRole("e927760d-1f10-4008-82fd-55f337cab8b7", "Employee"); //zaluski
            UserManager.AddToRole("c85cad58-aa35-45fe-a067-cd98cd404870", "Lab");
            UserManager.AddToRole("89445ac1-0503-47a8-9c96-67f02ad05a90", "Customer");
        }

        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data.
    }
    
}
