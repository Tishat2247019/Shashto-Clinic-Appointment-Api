namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelogin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tokens", "Login_Id", "dbo.Logins");
            DropIndex("dbo.Tokens", new[] { "Login_Id" });
            RenameColumn(table: "dbo.Tokens", name: "Login_Id", newName: "LoginId");
            AlterColumn("dbo.Tokens", "LoginId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tokens", "LoginId");
            AddForeignKey("dbo.Tokens", "LoginId", "dbo.Logins", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "LoginId", "dbo.Logins");
            DropIndex("dbo.Tokens", new[] { "LoginId" });
            AlterColumn("dbo.Tokens", "LoginId", c => c.Int());
            RenameColumn(table: "dbo.Tokens", name: "LoginId", newName: "Login_Id");
            CreateIndex("dbo.Tokens", "Login_Id");
            AddForeignKey("dbo.Tokens", "Login_Id", "dbo.Logins", "Id");
        }
    }
}
