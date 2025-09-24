using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class ConstructionFile: Entity
    {
        public string FileUrl { get; set; }=string.Empty;
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public virtual Item Item { get; set; }

        [ForeignKey("Item")]
        public Guid ItemId { get; set; }



    }
}
