namespace AutoParts.Controllers
{
    using AutoParts.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class CarModelsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetByManufacturer([FromQuery] int manufacturerId)
        {
            var models = await _context.CarModels
                .Where(c => c.ManufacturerId == manufacturerId)
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.YearInProductionSince,
                    c.YearInProductionTill
                })
                .ToListAsync();

            return Ok(models);
        }
    }
}