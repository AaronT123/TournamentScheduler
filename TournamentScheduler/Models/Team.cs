using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentScheduler.Models
{
    [Table("Team")]
    public class Team
    {
        [Key]
        public int TeamID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [ForeignKey("Tournament")]
        public int TournamentID { get; set; }
        public Tournament Tournament { get; set; }
    }
}