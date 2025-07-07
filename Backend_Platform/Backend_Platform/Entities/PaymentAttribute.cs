using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class PaymentAttribute : Entity
    {
        public string Key { get; set; } = string.Empty;
        public Guid PaymentMethodId { get; set; }
        [JsonIgnore]
        public virtual PaymentMethod PaymentMethod { get; set; }

        [JsonIgnore]
        public virtual ICollection<PaymentValue> Values { get; set; }
    }
}
