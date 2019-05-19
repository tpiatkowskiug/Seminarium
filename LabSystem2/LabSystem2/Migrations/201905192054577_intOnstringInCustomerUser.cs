namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intOnstringInCustomerUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Customers", "ApplicationUserId");
            RenameColumn(table: "dbo.Customers", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.Customers", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Customers", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Customers", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Customers", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "ApplicationUser_Id");
        }
    }
}
