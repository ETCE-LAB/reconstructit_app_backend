using System.Text.Json.Serialization;

namespace Backend_Platform.Entities
{
    public class PaymentValue : Entity
    {
        public string Value { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual PaymentAttribute PaymentAttribute { get; set; }
        public Guid PaymentAttributeId { get; set; }
        [JsonIgnore]
        public virtual Payment Payment { get; set; }
        public Guid PaymentId { get; set; }
    }
}
