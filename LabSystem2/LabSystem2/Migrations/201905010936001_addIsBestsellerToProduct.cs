namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsBestsellerToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsBestseller", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsBestseller");
        }
    }
}
