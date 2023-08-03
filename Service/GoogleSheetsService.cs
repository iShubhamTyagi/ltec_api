using System.Text.Json;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using LTEC.Interfaces;
using LTEC.Model;

namespace LTEC.Service;

public class GoogleSheetsService: IGoogleSheetsService
{
    private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private const string _applicationName = "LTEC";
    private const string _spreadsheetId = "1aNMQjLL1VpxOlO7QTN-ILFNdkCpPUmvFx6FpxvAP1Bs";
    private const string _sheet = "Sheet1";

    private readonly SheetsService _sheetsService;
    private readonly ILogger<GoogleSheetsService> _logger;

    public GoogleSheetsService(ILogger<GoogleSheetsService> logger)
    {
        _logger = logger;
        string folderName = "Configs";
        string fileName = "ltec-service-account-key.json";
        string CredentialFilePath = Path.Combine(folderName, fileName);

        var credential = GetGoogleCredential(CredentialFilePath);

        _sheetsService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = _applicationName,
        });
    }

    private GoogleCredential GetGoogleCredential(string filePath)
    {
        try
        {
            var json = File.ReadAllText(filePath);
            return GoogleCredential.FromJson(json)
                .CreateScoped(Scopes);
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "An error occurred while reading the service account key.");
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "An error occurred while deserializing the service account key.");
            throw;
        }
    }

    public async Task AppendDataToSheetAsync(PatientData data)
    {
        try
        {
            ValueRange body = PrepareData(data);
            var request = _sheetsService.Spreadsheets.Values.Append(body, _spreadsheetId, _sheet);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
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
            data.Disease,
            data.Age,
            data.Id,
            data.Sex,
            // Alternate between answers and verdicts
            PrepareAnswersAndVerdicts(data),
            data.OverallVerdict,
            data.Timer.ToString()
        };

        IList<IList<object>> values = new List<IList<object>> { rowData };

        return new ValueRange() { Values = values };
    }

    private static IEnumerable<object> PrepareAnswersAndVerdicts(PatientData data)
    {
        for (int i = 1; i <= 4; i++)
        {
            string index = i.ToString();

            _ = data.Answers.TryGetValue(index, out var answersValue);
            yield return string.Join(", ", answersValue);

            _ = data.Verdicts.TryGetValue(index, out var verdictsValue);
            yield return verdictsValue;
        }
    }

}
