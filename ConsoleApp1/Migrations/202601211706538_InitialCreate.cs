namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountNumber = c.String(nullable: false, maxLength: 128),
                        IsRestricted = c.Boolean(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Advisor_Pesel = c.String(maxLength: 128),
                        TransactionOnHold_TransactionID = c.Guid(),
                    })
                .PrimaryKey(t => t.AccountNumber)
                .ForeignKey("dbo.Employees", t => t.Advisor_Pesel)
                .ForeignKey("dbo.Customers", t => t.AccountNumber)
                .ForeignKey("dbo.Transactions", t => t.TransactionOnHold_TransactionID)
                .Index(t => t.AccountNumber)
                .Index(t => t.Advisor_Pesel)
                .Index(t => t.TransactionOnHold_TransactionID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Pesel = c.String(nullable: false, maxLength: 128),
                        EmployeeID = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TelNumber = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Pesel);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        Recipient_AccountNumber = c.String(maxLength: 128),
                        Sender_AccountNumber = c.String(maxLength: 128),
                        Employee_Pesel = c.String(maxLength: 128),
                        Account_AccountNumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Accounts", t => t.Recipient_AccountNumber)
                .ForeignKey("dbo.Accounts", t => t.Sender_AccountNumber)
                .ForeignKey("dbo.Employees", t => t.Employee_Pesel)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountNumber)
                .Index(t => t.Recipient_AccountNumber)
                .Index(t => t.Sender_AccountNumber)
                .Index(t => t.Employee_Pesel)
                .Index(t => t.Account_AccountNumber);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Pesel = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TelNumber = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Pesel);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Account_AccountNumber", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "TransactionOnHold_TransactionID", "dbo.Transactions");
            DropForeignKey("dbo.Accounts", "AccountNumber", "dbo.Customers");
            DropForeignKey("dbo.Transactions", "Employee_Pesel", "dbo.Employees");
            DropForeignKey("dbo.Transactions", "Sender_AccountNumber", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "Recipient_AccountNumber", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "Advisor_Pesel", "dbo.Employees");
            DropIndex("dbo.Transactions", new[] { "Account_AccountNumber" });
            DropIndex("dbo.Transactions", new[] { "Employee_Pesel" });
            DropIndex("dbo.Transactions", new[] { "Sender_AccountNumber" });
            DropIndex("dbo.Transactions", new[] { "Recipient_AccountNumber" });
            DropIndex("dbo.Accounts", new[] { "TransactionOnHold_TransactionID" });
            DropIndex("dbo.Accounts", new[] { "Advisor_Pesel" });
            DropIndex("dbo.Accounts", new[] { "AccountNumber" });
            DropTable("dbo.Customers");
            DropTable("dbo.Transactions");
            DropTable("dbo.Employees");
            DropTable("dbo.Accounts");
        }
    }
}
