namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Team
    {
        public Team()
        {
            this.Members = new List<UserTeam>();
            this.Invitations = new List<Invitation>();
            this.TeamEvents = new List<TeamEvent>();
        }

        [Key]
        [MinLength(0)]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Acronym { get; set; }

        [MinLength(0)]
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        public virtual ICollection<UserTeam> Members { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }

        public virtual ICollection<TeamEvent> TeamEvents { get; set; }
    }
}
