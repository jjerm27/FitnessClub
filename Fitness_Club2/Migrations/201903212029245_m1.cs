namespace Fitness_Club2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Filials",
                c => new
                    {
                        FilialId = c.Int(nullable: false, identity: true),
                        FilialName = c.String(nullable: false),
                        FilialAdress = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FilialId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Name_Room = c.String(nullable: false),
                        FilialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Filials", t => t.FilialId, cascadeDelete: true)
                .Index(t => t.FilialId);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        IdTraining = c.Int(nullable: false, identity: true),
                        Training_Name = c.String(),
                        TrainerId = c.String(nullable: false, maxLength: 128),
                        RoomId = c.Int(nullable: false),
                        TimeStart = c.DateTime(nullable: false),
                        TimeStop = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdTraining)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.TrainerId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Sex = c.String(nullable: false),
                        Specialize = c.String(),
                        Working_hours = c.String(),
                        Filial_Id = c.Int(nullable: false),
                        IsActive = c.String(nullable: false),
                        BirthDay = c.DateTime(nullable: false),
                        Adress = c.String(nullable: false),
                        Photo = c.String(nullable: false),
                        Date_Of_Create = c.DateTime(nullable: false),
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
                        Filial_FilialId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Filials", t => t.Filial_FilialId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Filial_FilialId);
            
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
                "dbo.TrainingUsers",
                c => new
                    {
                        IdTU = c.Int(nullable: false, identity: true),
                        IdTraining = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdTU)
                .ForeignKey("dbo.Trainings", t => t.IdTraining, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.IdTraining)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TrainingUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TrainingUsers", "IdTraining", "dbo.Trainings");
            DropForeignKey("dbo.Trainings", "TrainerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Filial_FilialId", "dbo.Filials");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trainings", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "FilialId", "dbo.Filials");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TrainingUsers", new[] { "UserId" });
            DropIndex("dbo.TrainingUsers", new[] { "IdTraining" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Filial_FilialId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Trainings", new[] { "RoomId" });
            DropIndex("dbo.Trainings", new[] { "TrainerId" });
            DropIndex("dbo.Rooms", new[] { "FilialId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TrainingUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Trainings");
            DropTable("dbo.Rooms");
            DropTable("dbo.Filials");
        }
    }
}
