namespace TournamentScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoundNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fixture", "RoundNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fixture", "RoundNumber");
        }
    }
}
