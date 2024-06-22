using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrdersDelivery.Models
{
    public class Order
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Number { get; set; }

        public string SenderCity { get; set; }

        public string SenderAddress { get; set; }

        public string RecipientCity { get; set; }

        public string RecipientAddress { get; set; }

        public float Weight { get; set; }

        public DateTime PickupDate { get; set; }
    }
}
