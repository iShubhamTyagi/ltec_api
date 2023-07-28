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
        public async Task<IActionResult> Post([FromBody] JsonElement json)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<PatientData>(json.ToString());

                if (data != null)
                {
                    // Initialize the dictionaries
                    data.Answers1 = new Dictionary<string, string>();
                    data.Answers2 = new Dictionary<string, string>();
                    data.Answers3 = new Dictionary<string, string>();
                    data.Answers4 = new Dictionary<string, string>();

                    // Distribute the answers into the appropriate dictionary
                    foreach (var pair in data.Answers)
                    {
                        var key = pair.Key;
                        var value = pair.Value;

                        // Distribute based on the prefix of the key
                        switch (key[0])
                        {
                            case '1':
                                data.Answers1[key] = value;
                                break;
                            case '2':
                                data.Answers2[key] = value;
                                break;
                            case '3':
                                data.Answers3[key] = value;
                                break;
                            case '4':
                                data.Answers4[key] = value;
                                break;
                        }
                    }

                    await _googleSheetService.AppendDataToSheetAsync(data);
                }

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