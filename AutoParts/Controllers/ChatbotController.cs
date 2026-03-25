namespace AutoParts.Controllers
{
    using AutoParts.Data;
    using AutoParts.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController(AppDbContext context, ChatbotService chatbotService) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly ChatbotService _chatbotService = chatbotService;

        [HttpPost("query")]
        public async Task<IActionResult> Query([FromBody] ChatQueryRequest request)
        {
            var carModel = await _context.CarModels
                .Include(c => c.Manufacturer)
                .FirstOrDefaultAsync(c => c.Id == request.CarModelId);

            if (carModel == null)
                return NotFound("Car model not found.");

            var parts = await _context.PartCompatibilities
                .Include(pc => pc.Part)
                    .ThenInclude(p => p.PartCategory)
                .Where(pc => pc.CarModelId == request.CarModelId)
                .Select(pc => pc.Part)
                .ToListAsync();

            if (!parts.Any())
                return NotFound("No compatible parts found for this car model.");

            string fullName = $"{carModel.Manufacturer.Name} {carModel.Name}";

            var response = await _chatbotService.GenerateResponseAsync(
                request.Query,
                fullName,
                parts
            );

            return Ok(new { response });
        }
    }

    public record ChatQueryRequest(string Query, int CarModelId);
}