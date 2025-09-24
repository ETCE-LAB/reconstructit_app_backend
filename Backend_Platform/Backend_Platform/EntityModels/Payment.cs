using Backend_Platform.Entities.enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
namespace Backend_Platform.Entities
{
    public class Payment : Entity
    {
        public PaymentStatus PaymentStatus { get; set; }

        [JsonIgnore]
        public virtual ICollection<PaymentValue> PaymentValues { get; set; }

        [JsonIgnore]
        public virtual PaymentMethod PaymentMethod { get; set; }
        public Guid PaymentMethodId { get; set; }

        [JsonIgnore]
        public virtual PrintContract PrintContract { get; set; }
        public Guid PrintContractId { get; set; }
    }
}
