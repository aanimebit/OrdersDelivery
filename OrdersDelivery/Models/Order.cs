using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrdersDelivery.Models
{
    public class Order
    {
        /*
         * I decided to use single table for storing Orders & Towns instead of individual tables 
         * for Towns and saving in the Order's table only Town's foreighn keys 
         * because i thought that if the system will be large the join operation 
         * over Order's table and Town's table will take up load.
         */

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
