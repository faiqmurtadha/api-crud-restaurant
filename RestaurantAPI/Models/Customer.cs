using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace RestaurantAPI.Models
{
    public class Customer
    {
        [Key]
        public int customerId { get; set; }
        public required string customerName { get; set; }
        public required string email { get; set; }
        public required string phoneNumber { get; set; }
        public List<Transaction>? transactions { get; set; }
    }
}
