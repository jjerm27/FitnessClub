namespace Fitness_Club2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteNameTraining : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trainings", "Training_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainings", "Training_Name", c => c.String());
        }
    }
}
