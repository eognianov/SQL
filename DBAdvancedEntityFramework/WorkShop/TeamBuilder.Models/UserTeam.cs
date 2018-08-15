namespace TeamBuilder.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserTeam
    {
        [MinLength(0)]
        public int UserId { get; set; }

        [MinLength(0)]
        public int TeamId { get; set; }

        public virtual User User { get; set; }

        public virtual Team Team { get; set; }
    }
}
