namespace JoinUsAPIv3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Address = c.String(nullable: false),
                        UrlFacebook = c.String(),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatorId = c.Long(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.EventCategories",
                c => new
                    {
                        Event_Id = c.Long(nullable: false),
                        Category_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_Id, t.Category_Id })
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Event_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.UserUsers",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        User_Id1 = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.User_Id1 })
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.UserCategories",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        Category_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Category_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        Event_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Event_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.TagEvents",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        Event_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Event_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Event_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TagEvents", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.TagEvents", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Events", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserEvents", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.UserEvents", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.UserCategories", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.EventCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.EventCategories", "Event_Id", "dbo.Events");
            DropIndex("dbo.TagEvents", new[] { "Event_Id" });
            DropIndex("dbo.TagEvents", new[] { "Tag_Id" });
            DropIndex("dbo.UserEvents", new[] { "Event_Id" });
            DropIndex("dbo.UserEvents", new[] { "User_Id" });
            DropIndex("dbo.UserCategories", new[] { "Category_Id" });
            DropIndex("dbo.UserCategories", new[] { "User_Id" });
            DropIndex("dbo.UserUsers", new[] { "User_Id1" });
            DropIndex("dbo.UserUsers", new[] { "User_Id" });
            DropIndex("dbo.EventCategories", new[] { "Category_Id" });
            DropIndex("dbo.EventCategories", new[] { "Event_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropIndex("dbo.Events", new[] { "CreatorId" });
            DropTable("dbo.TagEvents");
            DropTable("dbo.UserEvents");
            DropTable("dbo.UserCategories");
            DropTable("dbo.UserUsers");
            DropTable("dbo.EventCategories");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tags");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
            DropTable("dbo.Categories");
        }
    }
}
