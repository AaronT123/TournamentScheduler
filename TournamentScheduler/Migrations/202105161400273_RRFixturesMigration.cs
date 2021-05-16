namespace TournamentScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RRFixturesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fixture",
                c => new
                    {
                        RRFixtureID = c.Int(nullable: false, identity: true),
                        Team1ID = c.Int(nullable: false),
                        Team2ID = c.Int(nullable: false),
                        Team1Score = c.Int(nullable: false),
                        Team2Score = c.Int(nullable: false),
                        TournamentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RRFixtureID)
                .ForeignKey("dbo.Tournament", t => t.TournamentID, cascadeDelete: true)
                .Index(t => t.TournamentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fixture", "TournamentID", "dbo.Tournament");
            DropIndex("dbo.Fixture", new[] { "TournamentID" });
            DropTable("dbo.Fixture");
        }
    }
}
