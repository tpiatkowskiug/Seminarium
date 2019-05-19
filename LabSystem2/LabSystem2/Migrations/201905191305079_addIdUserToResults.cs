namespace LabSystem2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIdUserToResults : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResultsOfOrderGRs", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "EmployeeId" });
            RenameColumn(table: "dbo.ResultsOfOrderGRs", name: "ApplicationUser_Id", newName: "UserEmployee_Id");
            RenameIndex(table: "dbo.ResultsOfOrderGRs", name: "IX_ApplicationUser_Id", newName: "IX_UserEmployee_Id");
            AddColumn("dbo.ResultsOfOrderGRs", "Employee_EmployeeId", c => c.Int());
            AlterColumn("dbo.ResultsOfOrderGRs", "EmployeeId", c => c.String());
            CreateIndex("dbo.ResultsOfOrderGRs", "Employee_EmployeeId");
            AddForeignKey("dbo.ResultsOfOrderGRs", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultsOfOrderGRs", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.ResultsOfOrderGRs", new[] { "Employee_EmployeeId" });
            AlterColumn("dbo.ResultsOfOrderGRs", "EmployeeId", c => c.Int());
            DropColumn("dbo.ResultsOfOrderGRs", "Employee_EmployeeId");
            RenameIndex(table: "dbo.ResultsOfOrderGRs", name: "IX_UserEmployee_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.ResultsOfOrderGRs", name: "UserEmployee_Id", newName: "ApplicationUser_Id");
            CreateIndex("dbo.ResultsOfOrderGRs", "EmployeeId");
            AddForeignKey("dbo.ResultsOfOrderGRs", "EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
