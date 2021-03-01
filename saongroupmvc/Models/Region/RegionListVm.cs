using Newtonsoft.Json;
using System.Collections.Generic;

namespace saongroupmvc.Models.Regions
{
    public class RegionListVm
    {
        [JsonProperty(PropertyName = "data")]
        public List<RegionVm> regions { get; set; }
    }
}
