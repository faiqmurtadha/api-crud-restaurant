using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantAPI.Models
{
    public class Transaction
    {
        [Key]
        public int transactionId { get; set; }
        public int customerId { get; set; }
        public int foodId { get; set; }
        public required int qty { get; set; }
        public required int totalPrice { get; set; }
        public DateTime transactionDate { get; set; }

        [JsonIgnore]
        public Customer? customer { get; set; }

        [JsonIgnore]
        public Food? food { get; set; }
    }
}
