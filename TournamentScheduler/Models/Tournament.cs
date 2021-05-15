using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentScheduler.Models
{
    [Table("Tournament")]
    public class Tournament
    {
        [Key]
        public int TournamentID { get; set; }

        [Required]
        [Display(Name = "Tournament Name")]
        public string TournamentName { get; set; }

        [ScaffoldColumn(false)]
        public string TournamentOwnerID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}