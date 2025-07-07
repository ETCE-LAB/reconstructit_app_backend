using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class PaymentMethod : Entity
    {
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<PaymentAttribute> Attributes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
