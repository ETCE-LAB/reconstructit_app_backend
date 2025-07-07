using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class Chat:Entity
    {
        [JsonIgnore]
        public virtual ICollection<Participant> Participants { get; set; } = [];
        [JsonIgnore]
        public virtual CommunityPrintRequest CommunityPrintRequest { get; set; }    
      public Guid CommunityPrintRequestId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
        [JsonIgnore]
        public virtual Address? Address { get; set; }    
        public Guid? AddressId { get; set; }
        
    }
}
