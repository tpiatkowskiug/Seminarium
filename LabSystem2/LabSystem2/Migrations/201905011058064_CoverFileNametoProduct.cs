namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoverFileNametoProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CoverFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CoverFileName");
        }
    }
}
