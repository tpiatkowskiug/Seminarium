namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserData_Comment");
            DropColumn("dbo.AspNetUsers", "UserData_DateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserData_DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserData_Comment", c => c.String());
        }
    }
}
