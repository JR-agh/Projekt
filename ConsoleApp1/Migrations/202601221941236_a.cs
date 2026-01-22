namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "EmployeeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "EmployeeID", c => c.Int(nullable: false));
        }
    }
}
