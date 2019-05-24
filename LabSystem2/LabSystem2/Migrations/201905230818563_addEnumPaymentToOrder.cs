namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEnumPaymentToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Payment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Payment");
        }
    }
}
