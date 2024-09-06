using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace RestaurantAPI.Models
{
    public class Food
    {
        [Key]
        public int foodId { get; set; }
        public required string foodName { get; set; }
        public required int price { get; set; }
        public required int stock { get; set; }
        public List<Transaction>? transactions { get; set; }
    }
}
