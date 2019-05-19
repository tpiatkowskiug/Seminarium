namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOrderAndResults : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId", "dbo.ResultsOfOrderGRs");
            DropForeignKey("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId", "dbo.ResultsOfOrderGRs");
            DropIndex("dbo.OrderItems", new[] { "ResultsOfOrderGR_ResultsOfOrderGRId" });
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "ResultsOfOrderGR_ResultsOfOrderGRId" });
            DropColumn("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId");
            DropColumn("dbo.ResultsOfOrderGRs", "OrderStateRusalt");
            DropColumn("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId", c => c.Int());
            AddColumn("dbo.ResultsOfOrderGRs", "OrderStateRusalt", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId", c => c.Int());
            CreateIndex("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId");
            CreateIndex("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId");
            AddForeignKey("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId", "dbo.ResultsOfOrderGRs", "ResultsOfOrderGRId");
            AddForeignKey("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId", "dbo.ResultsOfOrderGRs", "ResultsOfOrderGRId");
        }
    }
}
