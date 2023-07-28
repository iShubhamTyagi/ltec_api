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

        // New properties
        public Dictionary<string, string> Answers1 { get; set; }
        public Dictionary<string, string> Answers2 { get; set; }
        public Dictionary<string, string> Answers3 { get; set; }
        public Dictionary<string, string> Answers4 { get; set; }
    }

}
