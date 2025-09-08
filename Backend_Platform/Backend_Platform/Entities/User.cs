using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class User: Entity
    {
        public string UserAccountId { get; set; }  = string.Empty;
        public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
public string Region { get; set; } = string.Empty;
        public string? UserProfilePictureUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual Address? Address { get; set; }
        [JsonIgnore]
        public virtual ICollection<Participant> Contracts { get; set; } = [];
        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; } = [];


            }
}
