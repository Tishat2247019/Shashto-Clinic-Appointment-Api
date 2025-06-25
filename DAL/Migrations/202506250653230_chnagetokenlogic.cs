namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chnagetokenlogic : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tokens", "UserId", "dbo.Logins");
            DropIndex("dbo.Tokens", new[] { "UserId" });
            AddColumn("dbo.Tokens", "UserType", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Tokens", "Login_Id", c => c.Int());
            CreateIndex("dbo.Tokens", "Login_Id");
            AddForeignKey("dbo.Tokens", "Login_Id", "dbo.Logins", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "Login_Id", "dbo.Logins");
            DropIndex("dbo.Tokens", new[] { "Login_Id" });
            DropColumn("dbo.Tokens", "Login_Id");
            DropColumn("dbo.Tokens", "UserType");
            CreateIndex("dbo.Tokens", "UserId");
            AddForeignKey("dbo.Tokens", "UserId", "dbo.Logins", "Id", cascadeDelete: true);
        }
    }
}
