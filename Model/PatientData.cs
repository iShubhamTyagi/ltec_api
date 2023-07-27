namespace LTEC.Model
{
    public class PatientData
    {
        public int SelectedSequence { get; set; }
        public string Age { get; set; }
        public string Id { get; set; }
        public string Sex { get; set; }
        public Dictionary<string, string> Answers { get; set; }
        public Dictionary<string, string> Verdicts { get; set; }
        public string OverallVerdict { get; set; }
        public int Timer { get; set; }
    }
}
