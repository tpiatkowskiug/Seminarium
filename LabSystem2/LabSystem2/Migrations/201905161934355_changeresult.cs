namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeresult : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId", "dbo.OrderItems");
            DropForeignKey("dbo.ResultsOfOrderGRs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "UserId" });
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "OrderItem_OrderItemId" });
            DropColumn("dbo.ResultsOfOrderGRs", "OrderItemId");
            DropColumn("dbo.ResultsOfOrderGRs", "UserId");
            DropColumn("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId", c => c.Int());
            AddColumn("dbo.ResultsOfOrderGRs", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.ResultsOfOrderGRs", "OrderItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId");
            CreateIndex("dbo.ResultsOfOrderGRs", "UserId");
            AddForeignKey("dbo.ResultsOfOrderGRs", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ResultsOfOrderGRs", "OrderItem_OrderItemId", "dbo.OrderItems", "OrderItemId");
        }
    }
}
