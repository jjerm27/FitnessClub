namespace Fitness_Club2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTraining : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainings", "TimeOfTraining", c => c.String(nullable: false));
            DropColumn("dbo.Trainings", "TimeStart");
            DropColumn("dbo.Trainings", "TimeStop");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainings", "TimeStop", c => c.DateTime(nullable: false));
            AddColumn("dbo.Trainings", "TimeStart", c => c.DateTime(nullable: false));
            DropColumn("dbo.Trainings", "TimeOfTraining");
        }
    }
}
