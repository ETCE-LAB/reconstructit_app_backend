using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class Address: Entity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Chat> Chats { get; set; } = [];
    }
}
