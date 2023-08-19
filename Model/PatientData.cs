namespace LTEC.Model
{
    public class PatientData
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Disease { get; set; }
        public string Age { get; set; }
        public string Id { get; set; }
        public string Sex { get; set; }
        public Dictionary<string, string> Answers { get; set; }
        public Dictionary<string, string> Verdicts { get; set; }
        public string OverallVerdict { get; set; }
        public int Duration { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }

        public Dictionary<string, string> Answers1 { get; set; }
        public Dictionary<string, string> Answers2 { get; set; }
        public Dictionary<string, string> Answers3 { get; set; }
        public Dictionary<string, string> Answers4 { get; set; }
    }

}
