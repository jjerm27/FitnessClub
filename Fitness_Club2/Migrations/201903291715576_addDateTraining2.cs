namespace Fitness_Club2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDateTraining2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainings", "dateOfTraining", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainings", "dateOfTraining", c => c.DateTime(nullable: false));
        }
    }
}
