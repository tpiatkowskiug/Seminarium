namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeResultofOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId", c => c.Int());
            AddColumn("dbo.ResultsOfOrderGRs", "OrderItemId", c => c.Int(nullable: false));
            AddColumn("dbo.ResultsOfOrderGRs", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ResultsOfOrderGRs", "OrderStateRusalt", c => c.Int(nullable: false));
            AddColumn("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId", c => c.Int());
            CreateIndex("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId");
            CreateIndex("dbo.ResultsOfOrderGRs", "UserId");
            CreateIndex("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId");
            AddForeignKey("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId", "dbo.OrderItems", "OrderItemId");
            AddForeignKey("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId", "dbo.ResultsOfOrderGRs", "ResultsOfOrderGRId");
            AddForeignKey("dbo.ResultsOfOrderGRs", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultsOfOrderGRs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId", "dbo.ResultsOfOrderGRs");
            DropForeignKey("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId", "dbo.OrderItems");
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "OrderItem_OrderItemId" });
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "UserId" });
            DropIndex("dbo.OrderItems", new[] { "ResultsOfOrderGR_ResultsOfOrderGRId" });
            DropColumn("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId");
            DropColumn("dbo.ResultsOfOrderGRs", "OrderStateRusalt");
            DropColumn("dbo.ResultsOfOrderGRs", "UserId");
            DropColumn("dbo.ResultsOfOrderGRs", "OrderItemId");
            DropColumn("dbo.OrderItems", "ResultsOfOrderGR_ResultsOfOrderGRId");
        }
    }
}
