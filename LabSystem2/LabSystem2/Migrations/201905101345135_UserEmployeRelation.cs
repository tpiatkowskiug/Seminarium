namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEmployeRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            RenameColumn(table: "dbo.Orders", name: "User_Id", newName: "UserEmployee_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_User_Id", newName: "IX_UserEmployee_Id");
            AddColumn("dbo.Orders", "Employee_EmployeeId", c => c.Int());
            AlterColumn("dbo.Orders", "EmployeeId", c => c.String());
            CreateIndex("dbo.Orders", "Employee_EmployeeId");
            AddForeignKey("dbo.Orders", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Orders", new[] { "Employee_EmployeeId" });
            AlterColumn("dbo.Orders", "EmployeeId", c => c.Int());
            DropColumn("dbo.Orders", "Employee_EmployeeId");
            RenameIndex(table: "dbo.Orders", name: "IX_UserEmployee_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Orders", name: "UserEmployee_Id", newName: "User_Id");
            CreateIndex("dbo.Orders", "EmployeeId");
            AddForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
