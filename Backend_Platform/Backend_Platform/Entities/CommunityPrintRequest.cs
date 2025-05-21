using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class CommunityPrintRequest:Entity
    {
        public double PriceMax { get; set; }
        [JsonIgnore]
        public  virtual Item Item { get; set; }
        [ForeignKey("Item")]
        public Guid ItemId { get; set; }
        [JsonIgnore]
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
