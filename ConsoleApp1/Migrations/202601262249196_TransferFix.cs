namespace ConsoleApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransferFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Account_OwnersPesel", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "Recipient_OwnersPesel" });
            DropIndex("dbo.Transactions", new[] { "Sender_OwnersPesel" });
            DropIndex("dbo.Transactions", new[] { "Account_OwnersPesel" });
            DropColumn("dbo.Transactions", "RecipientsPesel");
            DropColumn("dbo.Transactions", "SendersPesel");
            RenameColumn(table: "dbo.Transactions", name: "Recipient_OwnersPesel", newName: "RecipientsPesel");
            RenameColumn(table: "dbo.Transactions", name: "Sender_OwnersPesel", newName: "SendersPesel");
            AlterColumn("dbo.Transactions", "SendersPesel", c => c.String(maxLength: 128));
            AlterColumn("dbo.Transactions", "RecipientsPesel", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "SendersPesel");
            CreateIndex("dbo.Transactions", "RecipientsPesel");
            DropColumn("dbo.Transactions", "Account_OwnersPesel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Account_OwnersPesel", c => c.String(maxLength: 128));
            DropIndex("dbo.Transactions", new[] { "RecipientsPesel" });
            DropIndex("dbo.Transactions", new[] { "SendersPesel" });
            AlterColumn("dbo.Transactions", "RecipientsPesel", c => c.String());
            AlterColumn("dbo.Transactions", "SendersPesel", c => c.String());
            RenameColumn(table: "dbo.Transactions", name: "SendersPesel", newName: "Sender_OwnersPesel");
            RenameColumn(table: "dbo.Transactions", name: "RecipientsPesel", newName: "Recipient_OwnersPesel");
            AddColumn("dbo.Transactions", "SendersPesel", c => c.String());
            AddColumn("dbo.Transactions", "RecipientsPesel", c => c.String());
            CreateIndex("dbo.Transactions", "Account_OwnersPesel");
            CreateIndex("dbo.Transactions", "Sender_OwnersPesel");
            CreateIndex("dbo.Transactions", "Recipient_OwnersPesel");
            AddForeignKey("dbo.Transactions", "Account_OwnersPesel", "dbo.Accounts", "OwnersPesel");
        }
    }
}
