using LTEC.Interfaces;
using LTEC.Model;

namespace LTEC.Service
{
    public class PatientDataService : IPatientDataService
    {
        public PatientData DistributeAnswers(PatientData data)
        {
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
            }

            return data;
        }
    }

}
