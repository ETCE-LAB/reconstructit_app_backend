using System.Text.Json.Serialization;
using Backend_Platform.Entities.enums;

namespace Backend_Platform.Entities
{
    public class PrintContract : Entity
    {
        public PrintContractStatus ContractStatus { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
        [JsonIgnore]
        public virtual CommunityPrintRequest CommunityPrintRequest { get; set; }    
        public Guid CommunityPrintRequestId { get; set; }

        [JsonIgnore]
        public virtual Address? RevealedAddress { get; set; }
        public Guid? RevealedAddressId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Participant> Participants { get; set; }

        [JsonIgnore]
        public virtual Payment? Payment { get; set; }

        public Guid?  PaymentId { get; set; }
    }
}
