namespace JoinUsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Events", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Birthdate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.AspNetUsers", "LastLoc", c => c.String());
            AddColumn("dbo.AspNetUsers", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            CreateIndex("dbo.Categories", "ApplicationUser_Id");
            CreateIndex("dbo.Events", "ApplicationUser_Id");
            CreateIndex("dbo.Users", "ApplicationUser_Id");
            CreateIndex("dbo.Users", "ApplicationUser_Id1");
            AddForeignKey("dbo.Categories", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Users", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Users", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Users", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Users", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Users", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Categories", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "RowVersion");
            DropColumn("dbo.AspNetUsers", "LastLoc");
            DropColumn("dbo.AspNetUsers", "Birthdate");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.Users", "ApplicationUser_Id1");
            DropColumn("dbo.Users", "ApplicationUser_Id");
            DropColumn("dbo.Events", "ApplicationUser_Id");
            DropColumn("dbo.Categories", "ApplicationUser_Id");
        }
    }
}
