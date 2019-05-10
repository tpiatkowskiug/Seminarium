namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracja3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserData_NameAndSurname", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_City", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_PostalCode", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserData_Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserData_Email");
            DropColumn("dbo.AspNetUsers", "UserData_Phone");
            DropColumn("dbo.AspNetUsers", "UserData_PostalCode");
            DropColumn("dbo.AspNetUsers", "UserData_City");
            DropColumn("dbo.AspNetUsers", "UserData_NameAndSurname");
        }
    }
}
