using System.Text.Json;
using LTEC.Interfaces;
using LTEC.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ltec.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LtecController : ControllerBase
    {
        private readonly ILogger<LtecController> _logger;
        private readonly IGoogleSheetsService _googleSheetService;
        private readonly IPatientDataService _patientDataService;

        public LtecController(ILogger<LtecController> logger, IGoogleSheetsService googleSheetService, IPatientDataService patientDataService)
        {
            _logger = logger;
            _googleSheetService = googleSheetService;
            _patientDataService = patientDataService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonElement json)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<PatientData>(json.ToString());

                data = _patientDataService.DistributeAnswers(data);

                await _googleSheetService.AppendDataToSheetAsync(data);

                return Ok();
            }
            catch (System.Text.Json.JsonException ex)
            {
                _logger.LogError(ex, "An error occurred while deserializing the JSON data.");
                return BadRequest("The provided data could not be deserialized.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }

}