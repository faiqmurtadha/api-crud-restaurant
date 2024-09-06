using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public TransactionController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET All Transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransaction()
        {
            return await _context.Transactions.ToListAsync();
        }

        // Get Transaction
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // POST Transaction
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            var customer = await _context.Customers.FindAsync(transaction.customerId);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            var food = await _context.Foods.FindAsync(transaction.foodId);
            if (food == null)
            {
                return NotFound("Food not found");
            }

            // Calculate Total Price
            transaction.totalPrice = food.price * transaction.qty;

            // Set default Transaction Date
            if (transaction.transactionDate == default)
            {
                transaction.transactionDate = DateTime.UtcNow;
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.transactionId }, transaction);
        }

        // PUT Transaction
        [HttpPut("{id}")]
        public async Task<ActionResult<Transaction>> PutTransaction(int id , Transaction transaction)
        {
            if (id != transaction.transactionId)
            {
                return BadRequest();
            }

            var customer = await _context.Customers.FindAsync(transaction.customerId);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            var food = await _context.Foods.FindAsync(transaction.foodId);
            if (food == null)
            {
                return NotFound("Food not found");
            }

            // Calculate Total Price
            transaction.totalPrice = food.price * transaction.qty;

            _context.Entry(transaction).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionIsExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE Transaction
        [HttpDelete]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Check Transaction is Exist
        private bool TransactionIsExist(int id)
        {
            return _context.Transactions.Any(e => e.transactionId == id);
        }
    }
}
