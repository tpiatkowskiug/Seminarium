namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        NIP = c.String(),
                        NameAndSurname = c.String(nullable: false, maxLength: 150),
                        Address = c.String(),
                        PostalCode = c.String(),
                        City = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        PhonePreferred = c.Boolean(nullable: false),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        NameAndSurname = c.String(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Region = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        EmployeeId = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Comment = c.String(),
                        OrderState = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Employee_EmployeeId = c.Int(),
                        UserEmployee_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserEmployee_Id)
                .Index(t => t.CustomerId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.UserEmployee_Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        GenreId = c.Int(),
                        ProductId = c.Int(),
                        Quantity = c.Int(nullable: false),
                        MarkingSample = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.GenreId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IconFilename = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        GenreId = c.Int(nullable: false),
                        ProductTitle = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Description = c.String(),
                        PriceNetto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceBrutto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsHidden = c.Boolean(nullable: false),
                        IsBestseller = c.Boolean(nullable: false),
                        CoverFileName = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserData_Comment = c.String(),
                        UserData_DateCreated = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ResultsOfOrderGRs",
                c => new
                    {
                        ResultsOfOrderGRId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        OrderMarkingSample = c.String(),
                        OrderPriceList = c.String(),
                        WynikiBadanpH = c.String(),
                        WynikiBadanFosfor = c.String(),
                        WynikiBadanPotas = c.String(),
                        WynikiBadanMagnez = c.String(),
                        AddOrder = c.Boolean(nullable: false),
                        Cu = c.String(),
                        Fe = c.String(),
                        Mn = c.String(),
                        Zn = c.String(),
                        Bor = c.String(),
                        InneBadania = c.String(),
                        ResultsCreationDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.ResultsOfOrderGRId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ResultsOfOrderGRs", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ResultsOfOrderGRs", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserEmployee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Products", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Orders", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "EmployeeId" });
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "OrderId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Products", new[] { "GenreId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "GenreId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserEmployee_Id" });
            DropIndex("dbo.Orders", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ResultsOfOrderGRs");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Products");
            DropTable("dbo.Genres");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
        }
    }
}
