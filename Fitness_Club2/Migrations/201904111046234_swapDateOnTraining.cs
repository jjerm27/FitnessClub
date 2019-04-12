namespace Fitness_Club2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class swapDateOnTraining : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainings", "dateOfTraining", c => c.DateTime(nullable: false));
            DropColumn("dbo.Trainings", "date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainings", "date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Trainings", "dateOfTraining", c => c.String(nullable: false));
        }
    }
}
