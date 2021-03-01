using Newtonsoft.Json;
using System.Collections.Generic;

namespace saongroupmvc.Models.Reports
{
    public class DisplayReportListVm
    {
        [JsonProperty(PropertyName = "data")]
        public List<DisplayReportVm> reports { get; set; }

        public void Init () => reports = new List<DisplayReportVm>();
    }
}
