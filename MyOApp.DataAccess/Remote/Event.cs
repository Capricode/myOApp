namespace MyOApp.DataAccess.Client
{
    public class Event
    {
        public long date { get; set; }
        public string duration { get; set; }
        public string kind { get; set; }
        public string national { get; set; }
        public string region { get; set; }
        public string type { get; set; }
        public string map { get; set; }
        public string deadline { get; set; }
        public string entryportal { get; set; }
        public string name { get; set; }
        public string idSource { get; set; }
        public string url { get; set; }
        public string organiser { get; set; }
        public string eventCenter { get; set; }
        public string source { get; set; }
        public int? classification { get; set; }
        public float? eventCenterLatitude { get; set; }
        public float? eventCenterLongitude { get; set; }
        public bool day { get; set; }
        public bool night { get; set; }
        public long lastModification { get; set; }
        public int? resultsId { get; set; }
    }
}
