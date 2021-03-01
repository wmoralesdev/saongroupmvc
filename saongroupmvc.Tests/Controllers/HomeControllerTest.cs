using Microsoft.VisualStudio.TestTools.UnitTesting;
using saongroupmvc.Controllers;
using System.Web.Mvc;

namespace saongroupmvc.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Regions()
        {
            // Arrange
            HomeController controller = new HomeController();
            controller._apiController.UnitUri = "https://covid-api.com/api/";

            // Act
            var responseTask = controller.Regions();
            responseTask.Wait();

            PartialViewResult result = responseTask.Result as PartialViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
