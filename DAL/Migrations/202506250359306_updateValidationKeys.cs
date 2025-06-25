namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateValidationKeys : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "Reason", c => c.String(maxLength: 500, unicode: false));
            AlterColumn("dbo.Appointments", "Status", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.Doctors", "Name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Doctors", "Email", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Doctors", "Phone", c => c.String(nullable: false, maxLength: 15, unicode: false));
            AlterColumn("dbo.Doctors", "Speciality", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Doctors", "Department", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Patients", "Name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Patients", "Email", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Patients", "Phone", c => c.String(nullable: false, maxLength: 15, unicode: false));
            AlterColumn("dbo.Patients", "Gender", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.Patients", "Address", c => c.String(maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.Patients", "Gender", c => c.String(maxLength: 10));
            AlterColumn("dbo.Patients", "Phone", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Patients", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Doctors", "Department", c => c.String(maxLength: 100));
            AlterColumn("dbo.Doctors", "Speciality", c => c.String(maxLength: 100));
            AlterColumn("dbo.Doctors", "Phone", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Doctors", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Appointments", "Status", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Appointments", "Reason", c => c.String(maxLength: 500));
        }
    }
}
