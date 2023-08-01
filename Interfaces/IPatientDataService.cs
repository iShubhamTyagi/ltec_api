using LTEC.Model;

namespace LTEC.Interfaces
{
    public interface IPatientDataService
    {
        PatientData DistributeAnswers(PatientData data);
    }
}
