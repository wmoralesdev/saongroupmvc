using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace saongroupmvc.Models.Province
{
    public class ProvinceListVm
    {
        [JsonProperty(PropertyName = "data")]
        public List<ProvinceVm> provinces { get; set; }
    }
}