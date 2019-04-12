namespace Fitness_Club2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateToTraining : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainings", "date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainings", "date");
        }
    }
}
