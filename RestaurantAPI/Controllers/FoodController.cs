using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public FoodController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET All Food
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetAllFood()
        {
            return await _context.Foods.ToListAsync();
        }

        // GET Food
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        // POST Food
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFood), new { id = food.foodId }, food);
        }

        // PUT Food
        [HttpPut]
        public async Task<ActionResult<Food>> PutFood(int id, Food food)
        {
            if (id != food.foodId)
            {
                return BadRequest();
            }

            _context.Entry(food).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodIsExist(id))
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

        // DELETE Food
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFood(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Check Food is Exist
        private bool FoodIsExist(int id)
        {
            return _context.Foods.Any(e => e.foodId == id);
        }
    }
}
