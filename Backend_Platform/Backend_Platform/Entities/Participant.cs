using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class Participant : Entity
    {
        public ParticipantRole Role { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; } 
        public Guid? UserId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
        [JsonIgnore]
        public virtual Chat Chat { get; set; }
        public Guid ChatId { get; set; }
    }
}
