namespace TeamBuilder.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TeamEvent
    {
        [MinLength(0)]
        public int TeamId { get; set; }

        [MinLength(0)]
        public int EventId { get; set; }

        public virtual Team Team { get; set; }

        public virtual Event Event { get; set; }
    }
}
