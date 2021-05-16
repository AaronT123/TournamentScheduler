using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentScheduler.Models
{
    [Table("Fixture")]
    public class RRFixture
    {
        [Key]
        public int RRFixtureID { get; set; }

        public int Team1ID { get; set; }

        public int Team2ID { get; set; }

        public int Team1Score { get; set; }

        public int Team2Score { get; set; }

        [ForeignKey("Tournament")]
        public int TournamentID { get; set; }
        public Tournament Tournament { get; set; }
    }
}