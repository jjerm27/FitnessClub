namespace Fitness_Club2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkingTimes",
                c => new
                    {
                        WorkingTimeId = c.Int(nullable: false, identity: true),
                        NameOfChema = c.String(nullable: false),
                        WorkingPeriodMinutes = c.Int(nullable: false),
                        RelaxPeriodMinutes = c.Int(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WorkingTimeId);
            
            AddColumn("dbo.AspNetUsers", "WorkingTimeId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "WorkingTimeId");
            AddForeignKey("dbo.AspNetUsers", "WorkingTimeId", "dbo.WorkingTimes", "WorkingTimeId", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "Working_hours");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Working_hours", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "WorkingTimeId", "dbo.WorkingTimes");
            DropIndex("dbo.AspNetUsers", new[] { "WorkingTimeId" });
            DropColumn("dbo.AspNetUsers", "WorkingTimeId");
            DropTable("dbo.WorkingTimes");
        }
    }
}
