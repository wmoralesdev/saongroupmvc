using Newtonsoft.Json;

namespace saongroupmvc.Models.Reports
{
    public class ReportVm
    {
        [JsonProperty(PropertyName = "confirmed")]
        public int Cases { get; set; }

        
        [JsonProperty(PropertyName = "deaths")]
        public int Deaths { get; set; }
    }
}