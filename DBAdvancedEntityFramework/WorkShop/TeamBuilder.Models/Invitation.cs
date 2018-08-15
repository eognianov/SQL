namespace TeamBuilder.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Invitation
    {
        public Invitation()
        {
            this.IsActive = true;
        }

        [Key]
        [MinLength(0)]
        public int Id { get; set; }

        [MinLength(0)]
        public int InvitedUserId { get; set; }

        [ForeignKey("InvitedUserId")]
        public virtual User InvitedUser { get; set; }

        [MinLength(0)]
        public int TeamId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }

        public bool IsActive { get; set; }
    }
}
