namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        СountryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserSurname = c.String(),
                        DayOfBirth = c.DateTime(),
                        UsersCountryId = c.Int(),
                        UserLogin = c.String(),
                        UserPassword = c.String(),
                        PhotoURL = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.User_Dialog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DialogId = c.Int(nullable: false),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.DialogId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.DialogId)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DialogName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DialogId = c.Int(nullable: false),
                        MessageText = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        DateOfChange = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.DialogId, cascadeDelete: true)
                .Index(t => t.DialogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Dialog", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.User_Dialog", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.MessageLists", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.Users", "Country_Id", "dbo.Countries");
            DropIndex("dbo.MessageLists", new[] { "DialogId" });
            DropIndex("dbo.User_Dialog", new[] { "Users_Id" });
            DropIndex("dbo.User_Dialog", new[] { "DialogId" });
            DropIndex("dbo.Users", new[] { "Country_Id" });
            DropTable("dbo.MessageLists");
            DropTable("dbo.Dialogs");
            DropTable("dbo.User_Dialog");
            DropTable("dbo.Users");
            DropTable("dbo.Countries");
        }
    }
}
