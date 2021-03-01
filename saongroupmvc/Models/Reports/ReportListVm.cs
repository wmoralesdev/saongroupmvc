using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace saongroupmvc.Models.Reports
{
    public class ReportListVm
    {
        [JsonProperty(PropertyName = "data")]
        public List<ReportVm> items { get; set; }
    }
}