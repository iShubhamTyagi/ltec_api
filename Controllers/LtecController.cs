using System.Text.Json;
using Ltec;
using LTEC.Model;
using LTEC.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ltec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LtecController : ControllerBase
    {
        private readonly ILogger<LtecController> _logger;
        private readonly GoogleSheetsService _googleSheetService;

        public LtecController(ILogger<LtecController> logger)
        {
            _logger = logger;
            _googleSheetService = new GoogleSheetsService(logger);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatientData data)
        {
            try
            {
                // Check if the data is null
                if (data == null)
                {
                    _logger.LogError("Received a null request body.");
                    return BadRequest("Request body is null.");
                }

                // Do any other validation on the data here...

                // Make the AppendDataToSheet method async and await it here
                await _googleSheetService.AppendDataToSheetAsync(data);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}