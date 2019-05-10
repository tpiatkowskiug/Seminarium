namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserData_NIP", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_PhonePreferred", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserData_Comment", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_DateCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "FirstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "FirstName", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserData_DateCreated");
            DropColumn("dbo.AspNetUsers", "UserData_Comment");
            DropColumn("dbo.AspNetUsers", "UserData_PhonePreferred");
            DropColumn("dbo.AspNetUsers", "UserData_NIP");
        }
    }
}
