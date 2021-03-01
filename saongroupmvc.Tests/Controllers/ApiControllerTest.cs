using Microsoft.VisualStudio.TestTools.UnitTesting;
using saongroupmvc.Controllers.Api;
using saongroupmvc.Models.Province;
using saongroupmvc.Models.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saongroupmvc.Tests.Controllers
{
    [TestClass]
    public class ApiControllerTest
    {
        [TestMethod]
        public void GetRegions()
        {
            ApiController api = new ApiController();
            api.UnitUri = "https://covid-api.com/api/";

            var responseTask = api.GetRegions();
            responseTask.Wait();

            var regions = responseTask.Result;

            Assert.IsNotNull(regions);
            Assert.IsInstanceOfType(regions, typeof(RegionListVm));
        }

        [TestMethod]
        public void GetProvinces()
        {
            ApiController api = new ApiController();
            api.UnitUri = "https://covid-api.com/api/";

            var responseTask = api.GetProvinces("JPN");
            responseTask.Wait();

            var provinces = responseTask.Result;

            Assert.IsNotNull(provinces);
            Assert.IsInstanceOfType(provinces, typeof(ProvinceListVm));
        }

        [TestMethod]
        public void GetDataFromRegion()
        {
            ApiController api = new ApiController();
            api.UnitUri = "https://covid-api.com/api/";

            var responseTask = api.GetDataFromRegion("Japan");
            responseTask.Wait();

            var region = responseTask.Result;
            Assert.IsNotNull(region);
        }

        [TestMethod]
        public void GetDataFromProvince()
        {
            ApiController api = new ApiController();
            api.UnitUri = "https://covid-api.com/api/";

            var responseTask = api.GetDataFromProvince("JPN", "Tokyo");
            responseTask.Wait();

            var province = responseTask.Result;
            Assert.IsNotNull(province);
        }
    }
}
