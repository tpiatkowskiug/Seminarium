namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRusalt : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ResultsOfOrderGRs", "OrderPriceList");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResultsOfOrderGRs", "OrderPriceList", c => c.String());
        }
    }
}
