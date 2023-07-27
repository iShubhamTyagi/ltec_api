using System.Text.Json;
using Ltec;
using Microsoft.AspNetCore.Mvc;

namespace Ltec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LtecController : ControllerBase
    {
        private readonly ILogger<LtecController> _logger;
        public LtecController(ILogger<LtecController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonElement json)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}