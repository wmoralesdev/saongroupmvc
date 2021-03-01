using Newtonsoft.Json;
using saongroupmvc.Controllers.Api;
using saongroupmvc.Models.Regions;
using saongroupmvc.Models.Reports;
using saongroupmvc.Extensions;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using CsvHelper;
using System.Globalization;
using saongroupmvc.Models.Exceptions;
using System;

namespace saongroupmvc.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApiController _apiController;
        private static RegionListVm _regionOptions = null;
        private static DisplayReportListVm _regions = null;
        public static DisplayReportListVm _actualData = null;

        public HomeController() : base()
        {
            _apiController = new ApiController();
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Regions()
        {
            try
            {
                if (_regionOptions is null)
                    _regionOptions = await _apiController.GetRegions();

                return PartialView("~/Views/Regions/RegionSelect.cshtml", _regionOptions);
            }
            catch (EmptyOrBadApiCallException)
            {
                return View("Error.cshtml", _regionOptions);
            }
        }

        public async Task<ActionResult> RegionCases()
        {
            try
            {
                if (_regions is null)
                {
                    _regions = new DisplayReportListVm();
                    _regions.Init();

                    foreach (var region in _regionOptions.regions)
                    {
                        var generalData = await _apiController.GetDataFromRegion(region.Name);

                        if (!(generalData is null))
                        {
                            _regions.reports.Add(new DisplayReportVm
                            {
                                Name = region.Name,
                                Cases = generalData.Cases,
                                Deaths = generalData.Deaths
                            });
                        }
                    }

                    _regions.reports = _regions.reports.OrderByDescending(x => x.Deaths).ToList();
                    _regions.reports = _regions.reports.GetRange(0, 10);

                    _actualData = _regions;
                }

                return PartialView("~/Views/Reports/Report.cshtml", _actualData);
            }
            catch (EmptyOrBadApiCallException)
            {
                return View("Error.cshtml", _regionOptions);
            }
        }

        public async Task<ActionResult> ProvinceCases(string iso)
        {
            try
            {
                var provinces = await _apiController.GetProvinces(iso);

                var provincesList = new DisplayReportListVm();
                provincesList.Init();

                foreach (var province in provinces.provinces)
                {
                    var generalData = await _apiController.GetDataFromProvince(iso, province.Province);

                    if (!(generalData is null))
                    {
                        provincesList.reports.Add(new DisplayReportVm
                        {
                            Name = province.Province,
                            Cases = generalData.Cases,
                            Deaths = generalData.Deaths
                        });
                    }
                }

                provincesList.reports = provincesList.reports.OrderByDescending(x => x.Deaths).ToList();
                provincesList.reports = provincesList.reports.Count >= 10 ? provincesList.reports.GetRange(0, 10) : provincesList.reports;

                _actualData = provincesList;

                return PartialView("~/Views/Reports/Report.cshtml", _actualData);
            }
            catch (EmptyOrBadApiCallException)
            {
                return View("Error.cshtml", _regionOptions);
            }
        }

        public ActionResult DownloadJsonFile()
        {
            try
            {
                var json = JsonConvert.SerializeObject(_actualData);

                return File(json.Encode(), "application/json", "data.json");
            }
            catch (Exception)
            {
                return View("Error.cshtml", _regionOptions);
            }
        }

        public ActionResult DownloadXmlFile()
        {
            try
            {
                var xmlSubmiter = new XmlSerializer(typeof(DisplayReportListVm));
                var xml = string.Empty;

                using (var strWriter = new StringWriter())
                {
                    using (var xmlWriter = new XmlTextWriter(strWriter)
                    {
                        Formatting = System.Xml.Formatting.Indented
                    })
                    {
                        xmlSubmiter.Serialize(xmlWriter, _actualData);
                        xml = strWriter.ToString();
                    }
                }

                return File(xml.Encode(), "text/xml", "data.xml");
            }
            catch (Exception)
            {
                return View("Error.cshtml", _regionOptions);
            }
        }

        public ActionResult DownloadCsvFile()
        {
            try
            {
                var csv = string.Empty;
                csv += "Location,Confirmed,Deaths\n";

                _actualData.reports.ForEach(item => csv += $"{item.Name},{item.Cases},{item.Deaths}\n");

                return File(csv.Encode(), "text/csv", "data.csv");
            }
            catch (Exception)
            {
                return View("Error.cshtml", _regionOptions);
            }
        }
    }
}