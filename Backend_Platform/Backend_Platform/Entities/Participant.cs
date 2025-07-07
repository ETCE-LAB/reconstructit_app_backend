using System.Text.Json.Serialization;
using Backend_Platform.Entities.enums;

namespace Backend_Platform.Entities
{
    public class Participant : Entity
    {
        public ParticipantRole Role { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; } 
        public Guid? UserId { get; set; }
        [JsonIgnore]
        public virtual PrintContract PrintContract { get; set; }
        public Guid PrintContractId { get; set; }
    }
}
