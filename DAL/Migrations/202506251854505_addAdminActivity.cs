namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAdminActivity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminId = c.Int(nullable: false),
                        Action = c.String(nullable: false, maxLength: 100),
                        Details = c.String(maxLength: 255),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: true)
                .Index(t => t.AdminId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdminActivities", "AdminId", "dbo.Admins");
            DropIndex("dbo.AdminActivities", new[] { "AdminId" });
            DropTable("dbo.AdminActivities");
        }
    }
}
