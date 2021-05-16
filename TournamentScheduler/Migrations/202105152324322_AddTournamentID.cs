namespace TournamentScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTournamentID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Team", "tournament_TournamentID", "dbo.Tournament");
            DropIndex("dbo.Team", new[] { "tournament_TournamentID" });
            RenameColumn(table: "dbo.Team", name: "tournament_TournamentID", newName: "TournamentID");
            AlterColumn("dbo.Team", "TournamentID", c => c.Int( defaultValue: 1));
            CreateIndex("dbo.Team", "TournamentID");
            AddForeignKey("dbo.Team", "TournamentID", "dbo.Tournament", "TournamentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Team", "TournamentID", "dbo.Tournament");
            DropIndex("dbo.Team", new[] { "TournamentID" });
            AlterColumn("dbo.Team", "TournamentID", c => c.Int());
            RenameColumn(table: "dbo.Team", name: "TournamentID", newName: "tournament_TournamentID");
            CreateIndex("dbo.Team", "tournament_TournamentID");
            AddForeignKey("dbo.Team", "tournament_TournamentID", "dbo.Tournament", "TournamentID");
        }
    }
}
