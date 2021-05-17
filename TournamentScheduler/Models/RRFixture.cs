using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [ScaffoldColumn(false)]
        public int RoundNumber { get; set; }

        [ScaffoldColumn(false)]
        public string Team1Name { get; set; }

        [ScaffoldColumn(false)]
        public string Team2Name { get; set; }

        [DefaultValue(0)]
        [Range(0, 1,
        ErrorMessage = "Value must be 0 or 1")]
        public int Team1Score { get; set; }

        [DefaultValue(0)]
        [Range(0, 1,
        ErrorMessage = "Value must be 0 or 1")]
        public int Team2Score { get; set; }

        [ScaffoldColumn(false)]
        public int TournamentID { get; set; }
        public Tournament Tournament { get; set; }
    }
}