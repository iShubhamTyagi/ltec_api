using LTEC.Model;

namespace LTEC.Interfaces
{
    public interface IGoogleSheetsService
    {
        Task AppendDataToSheetAsync(PatientData data);
    }

}
