namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationWithResultUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ResultsOfOrderGRId", c => c.Int(nullable: false));
            AddColumn("dbo.ResultsOfOrderGRs", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ResultsOfOrderGRs", "ApplicationUser_Id");
            AddForeignKey("dbo.ResultsOfOrderGRs", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultsOfOrderGRs", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ResultsOfOrderGRs", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "ResultsOfOrderGRId");
        }
    }
}
