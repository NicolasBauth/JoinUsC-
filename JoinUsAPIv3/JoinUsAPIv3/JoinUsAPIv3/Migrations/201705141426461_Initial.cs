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
                        Address = c.String(nullable: false),
                        UrlFacebook = c.String(),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatorId = c.Long(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        User_Id = c.Long(),
                        User_Id1 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .ForeignKey("dbo.Users", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Birthdate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Event_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.Event_Id);
            
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
            DropForeignKey("dbo.TagEvents", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.TagEvents", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Users", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Events", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Events", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.UserCategories", "User_Id", "dbo.Users");
            DropForeignKey("dbo.EventCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.EventCategories", "Event_Id", "dbo.Events");
            DropIndex("dbo.TagEvents", new[] { "Event_Id" });
            DropIndex("dbo.TagEvents", new[] { "Tag_Id" });
            DropIndex("dbo.UserUsers", new[] { "User_Id1" });
            DropIndex("dbo.UserUsers", new[] { "User_Id" });
            DropIndex("dbo.UserCategories", new[] { "Category_Id" });
            DropIndex("dbo.UserCategories", new[] { "User_Id" });
            DropIndex("dbo.EventCategories", new[] { "Category_Id" });
            DropIndex("dbo.EventCategories", new[] { "Event_Id" });
            DropIndex("dbo.Users", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "User_Id1" });
            DropIndex("dbo.Events", new[] { "User_Id" });
            DropIndex("dbo.Events", new[] { "CreatorId" });
            DropTable("dbo.TagEvents");
            DropTable("dbo.UserUsers");
            DropTable("dbo.UserCategories");
            DropTable("dbo.EventCategories");
            DropTable("dbo.Tags");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
            DropTable("dbo.Categories");
        }
    }
}
