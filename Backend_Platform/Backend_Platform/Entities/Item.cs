using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class Item: Entity

    {
        public RepairStatus Status { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ICollection<ItemImage> Images { get; set; }
        [JsonIgnore]
        public virtual ConstructionFile? ConstructionFile { get; set; }
        public Guid? ConstructionFileId {  get; set; }
        [JsonIgnore]
        public virtual CommunityPrintRequest? CommunityPrintRequest { get; set; }
        public Guid? CommunityPrintRequestId { get; set; }
        [JsonIgnore]  
        public virtual User User { get; set; }  
        public Guid UserId { get; set; }

    }
}
