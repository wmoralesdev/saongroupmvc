using System.Linq;
using Newtonsoft.Json;
using saongroupmvc.Models.Exceptions;
using saongroupmvc.Models.Regions;
using saongroupmvc.Models.Reports;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using saongroupmvc.Models.Province;

namespace saongroupmvc.Controllers.Api
{
    public class ApiController
    {
        private HttpClient Client { get; set; }
        public string UnitUri { get; set; }

        public async Task<RegionListVm> GetRegions()
        {
            RegionListVm regions = null;

            using (HttpClient Client = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"] ?? UnitUri)
            })
            {
                var result = await Client.GetAsync("regions");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    regions = JsonConvert.DeserializeObject<RegionListVm>(data);
                }
                else
                {
                    throw new EmptyOrBadApiCallException("Something went wrong while retrieving data from COVID API");
                }
            }

            return regions;
        }

        public async Task<ProvinceListVm> GetProvinces(string iso)
        {
            ProvinceListVm provinces = null;

            using (HttpClient Client = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"] ?? UnitUri)
            })
            {
                var result = await Client.GetAsync($"provinces/{iso}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    provinces = JsonConvert.DeserializeObject<ProvinceListVm>(data);

                    provinces.provinces.RemoveAll(pr => pr.Province == null || pr.Province == string.Empty);
                }
                else
                {
                    throw new EmptyOrBadApiCallException("Something went wrong while retrieving data from COVID API");
                }
            }

            return provinces;
        }

        public async Task<ReportVm> GetDataFromRegion(string country)
        {
            ReportListVm region = null;

            using (HttpClient Client = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"] ?? UnitUri)
            })
            {
                var result = await Client.GetAsync($"reports?region_name={country}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    region = JsonConvert.DeserializeObject<ReportListVm>(data);
                }
                else
                {
                    throw new EmptyOrBadApiCallException("Something went wrong while retrieving data from COVID API");
                }
            }

            return region.items.FirstOrDefault();
        }

        public async Task<ReportVm> GetDataFromProvince(string iso, string province)
        {
            ReportListVm region = null;

            using (HttpClient Client = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"] ?? UnitUri)
            })
            {
                var result = await Client.GetAsync($"reports?iso={iso}&region_province={province}");

                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync();
                    region = JsonConvert.DeserializeObject<ReportListVm>(data);
                }
                else
                {
                    throw new EmptyOrBadApiCallException("Something went wrong while retrieving data from COVID API");
                }
            }

            return region.items.FirstOrDefault();
        }
    }
}
