using System;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Ltec.Controllers;
using LTEC.Model;

namespace LTEC.Service;

public class GoogleSheetsService
{
    private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private const string _applicationName = "LTEC";
    private const string _spreadsheetId = "1aNMQjLL1VpxOlO7QTN-ILFNdkCpPUmvFx6FpxvAP1Bs";
    private const string _sheet = "Sheet1";

    private readonly SheetsService _sheetsService;

    private readonly ILogger<LtecController> _logger;
    //private object _logger;

    public GoogleSheetsService(ILogger<LtecController> logger)
    {
        _logger = logger;
        var json = File.ReadAllText(@"Configs\ltec-service-account-key.json");
        var credential = GoogleCredential.FromJson(json)
            .CreateScoped(Scopes);

        _sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = _applicationName,
        });
    }

    public async Task AppendDataToSheetAsync(PatientData data)
    {
        try
        {
            var body = PrepareData(data);
            var request = _sheetsService.Spreadsheets.Values.Append(body, _spreadsheetId, _sheet);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            // Make the call async
            await request.ExecuteAsync();
        }
        catch (GoogleApiException ex)
        {
            _logger.LogError(ex, "An error occurred while interacting with Google Sheets API.");
            throw;
        }
    }


    private static ValueRange PrepareData(PatientData data)
    {
        IList<object> rowData = new List<object>
        {
            data.SelectedSequence.ToString(),
            data.Age,
            data.Id,
            data.Sex,
            // Convert your Dictionary to a string as per your needs
            string.Join(", ", data.Answers),
            string.Join(", ", data.Verdicts),
            data.OverallVerdict,
            data.Timer.ToString()
        };

        IList<IList<object>> values = new List<IList<object>> { rowData };

        return new ValueRange() { Values = values };
    }
}