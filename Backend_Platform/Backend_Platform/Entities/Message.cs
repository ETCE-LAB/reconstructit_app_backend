using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class Message:Entity
    {
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public Guid? ParticipantId { get; set; }
        [JsonIgnore]
        public virtual  Participant? Sender { get; set; }
        [JsonIgnore]
        public virtual Chat Chat { get; set; }  
        public Guid ChatId { get; set; }
    }
}
