namespace TournamentScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixtureTeamIDtoName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fixture", "Team1Name", c => c.Int(nullable: false));
            AddColumn("dbo.Fixture", "Team2Name", c => c.Int(nullable: false));
            DropColumn("dbo.Fixture", "Team1ID");
            DropColumn("dbo.Fixture", "Team2ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fixture", "Team2ID", c => c.Int(nullable: false));
            AddColumn("dbo.Fixture", "Team1ID", c => c.Int(nullable: false));
            DropColumn("dbo.Fixture", "Team2Name");
            DropColumn("dbo.Fixture", "Team1Name");
        }
    }
}
