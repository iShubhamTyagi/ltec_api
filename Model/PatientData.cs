namespace LTEC.Model
{
    public class PatientData
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Disease { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public Dictionary<string, string> Answers { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Verdicts { get; set; } = new Dictionary<string, string>();
        public string OverallVerdict { get; set; } = string.Empty;
        public int Duration { get; set; } = 0 ;

        public string Date { get; set; }  = string.Empty;
        public string Time { get; set; }  = string.Empty;

        public Dictionary<string, string> Answers1 { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Answers2 { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Answers3 { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Answers4 { get; set; } = new Dictionary<string, string>();
    }

}
