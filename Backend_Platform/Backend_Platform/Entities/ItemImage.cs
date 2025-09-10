using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class ItemImage: Entity
    {
        public string ImageUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual Item Item { get; set; }
        public Guid ItemId { get; set; }
    }
}
