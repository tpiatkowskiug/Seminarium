namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlistofResults : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId", c => c.Int());
            CreateIndex("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId");
            AddForeignKey("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId", "dbo.ResultsOfOrderGRs", "ResultsOfOrderGRId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId", "dbo.ResultsOfOrderGRs");
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "ResultsOfOrderGR_ResultsOfOrderGRId" });
            DropColumn("dbo.ResultsOfOrderGRs", "ResultsOfOrderGR_ResultsOfOrderGRId");
        }
    }
}
