using AutoMapper;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskSolution.AutoMapperProfiles;
using TaskSolution.Controllers;
using TaskSolution.ViewModels;

namespace TaskSolution.Tests.Controllers
{
    [TestFixture]
    public class CompanyManagementControllerTest
    {
        CompanyManagementController controller;
        Mock<ICompanyService> mockCompanyService;
        IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mockCompanyService = new Mock<ICompanyService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CompanyProfile());
            });
            mapper = config.CreateMapper();
        }

        #region Index
        [Test]
        public async Task Index_returns_companies_list()
        {
            List<Company> companies = new List<Company>();
            companies.Add(new Company { ComanyId = 1, CompanyName = "Company1", KRS = "000065465", NIP = "23312655", REGON = "79823132" });
            companies.Add(new Company { ComanyId = 1, CompanyName = "Company2", KRS = "000046134", NIP = "74513216", REGON = "65323314" });
            companies.Add(new Company { ComanyId = 1, CompanyName = "Company3", KRS = "000013322", NIP = "98713211", REGON = "21326498" });
            companies.Add(new Company { ComanyId = 1, CompanyName = "Company4", KRS = "000097126", NIP = "98754213", REGON = "14785923" });

            mockCompanyService.Setup(m => m.GetAllAsync()).ReturnsAsync(companies);

            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Index() as ViewResult;
            var resultModel = (List<CompanyViewModel>)result.Model;

            Assert.IsNotNull(result, "Action result is null");
            Assert.AreEqual(4, resultModel.Count(), "Invalid result count");
            Assert.AreEqual(companies[0].ComanyId, resultModel[0].ComanyId);
            Assert.AreEqual(companies[1].CompanyName, resultModel[1].CompanyName);
            Assert.AreEqual(companies[2].KRS, resultModel[2].KRS);
            Assert.AreEqual(companies[3].NIP, resultModel[3].NIP);
        }
        #endregion

        #region Details
        [Test]
        public async Task Details_returns_BadRequest_if_companyId_is_not_provided()
        {
            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Details() as HttpStatusCodeResult;

            Assert.That(result, Is.Not.Null, "Action result is null");
            Assert.That(result.StatusCode, Is.EqualTo(400),"Invalid status code");
        }

        [Test]
        public async Task Details_returns_NotFound_if_company_is_null()
        {
            Company company = null;
            mockCompanyService.Setup(m => m.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(company);

            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Details(12) as HttpStatusCodeResult;

            Assert.IsNotNull(result, "Result is null");
            Assert.That(result.StatusCode, Is.EqualTo(404), "Invalid status code");
        }

        [Test]
        public async Task Details_returns_correct_company_data()
        {
            Address address = new Address { CompanyId=1, City="TestCity", HouseNumber="22a", Street="TestStreet", ZipCode="44-444" };
            Company company = new Company { ComanyId = 1, CompanyName = "Company1", KRS = "000065465", NIP = "23312655", REGON = "79823132", CompanyAddress = address };

            mockCompanyService.Setup(m => m.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(company);

            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Details(1) as ViewResult;
            var resultModel = (CompanyDetailsViewModel)result.Model;

            Assert.IsNotNull(result, "Result is null");
            Assert.AreEqual(1, resultModel.ComanyId);
            Assert.AreEqual("Company1", resultModel.CompanyName);
            Assert.AreEqual("TestCity", resultModel.City);
        }
        #endregion

        #region Create
        [Test]
        public async Task Create_returns_model_state_error_if_model_state_is_not_valid()
        {
            controller = new CompanyManagementController(mockCompanyService.Object, mapper);
            controller.ModelState.Clear();
            controller.ModelState.AddModelError("", "Errror");

            var result = await controller.Create(It.IsAny<AddCompanyViewModel>()) as ViewResult;

            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
        }

        [Test]
        public async Task Create_adds_new_company_and_redirects_to_Index_action()
        {
            controller = new CompanyManagementController(mockCompanyService.Object, mapper);
            var result = await controller.Create(new AddCompanyViewModel ()) as RedirectToRouteResult;

            Assert.IsNotNull(result, "Result is null");
            Assert.AreEqual("Index", result.RouteValues["action"], "Invalid action name");
            mockCompanyService.Verify(m => m.CreateAsync(It.IsAny<Company>()), Times.Once);
        }
        #endregion

        #region Edit
        [Test]
        public async Task Edit_returns_BadRequest_if_companyId_is_not_provided()
        {
            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Edit() as HttpStatusCodeResult;

            Assert.That(result, Is.Not.Null, "Action result is null");
            Assert.That(result.StatusCode, Is.EqualTo(400), "Invalid status code");
        }

        [Test]
        public async Task Edit_returns_NotFound_if_company_is_null()
        {
            Company company = null;
            mockCompanyService.Setup(m => m.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(company);

            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Edit(12) as HttpStatusCodeResult;

            Assert.IsNotNull(result, "Result is null");
            Assert.That(result.StatusCode, Is.EqualTo(404), "Invalid status code");
        }

        [Test]
        public async Task Edit_returns_correct_company_data()
        {
            Address address = new Address { CompanyId = 1, City = "TestCity", HouseNumber = "22a", Street = "TestStreet", ZipCode = "44-444" };
            Company company = new Company { ComanyId = 1, CompanyName = "Company1", KRS = "000065465", NIP = "23312655", REGON = "79823132", CompanyAddress = address };

            mockCompanyService.Setup(m => m.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(company);

            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Edit(1) as ViewResult;
            var resultModel = (EditCompanyViewModel)result.Model;

            Assert.IsNotNull(result, "Result is null");
            Assert.AreEqual(1, resultModel.ComanyId);
            Assert.AreEqual("Company1", resultModel.CompanyName);
            Assert.AreEqual("TestCity", resultModel.City);
        }

        [Test]
        public async Task Edit_returns_model_state_error_if_model_state_is_not_valid()
        {
            controller = new CompanyManagementController(mockCompanyService.Object, mapper);
            controller.ModelState.Clear();
            controller.ModelState.AddModelError("", "Errror");

            var result = await controller.Edit(It.IsAny<EditCompanyViewModel>()) as ViewResult;

            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
        }

        [Test]
        public async Task Edit_updates_company_data_and_redirects_to_Index_action()
        {
            controller = new CompanyManagementController(mockCompanyService.Object, mapper);
            var result = await controller.Edit(new EditCompanyViewModel()) as RedirectToRouteResult;

            Assert.IsNotNull(result, "Result is null");
            Assert.AreEqual("Index", result.RouteValues["action"], "Invalid action name");
            mockCompanyService.Verify(m => m.UpdateAsync(It.IsAny<Company>()), Times.Once);
        }
        #endregion

        #region Delete
        [Test]
        public async Task Delete_returns_BadRequest_if_companyId_is_not_provided()
        {
            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Delete() as HttpStatusCodeResult;

            Assert.That(result, Is.Not.Null, "Action result is null");
            Assert.That(result.StatusCode, Is.EqualTo(400), "Invalid status code");
        }

        [Test]
        public async Task Delete_returns_NotFound_if_company_is_null()
        {
            Company company = null;
            mockCompanyService.Setup(m => m.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(company);

            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Delete(12) as HttpStatusCodeResult;

            Assert.IsNotNull(result, "Result is null");
            Assert.That(result.StatusCode, Is.EqualTo(404), "Invalid status code");
        }

        [Test]
        public async Task Delete_returns_correct_company_data()
        {
            Address address = new Address { CompanyId = 1, City = "TestCity", HouseNumber = "22a", Street = "TestStreet", ZipCode = "44-444" };
            Company company = new Company { ComanyId = 1, CompanyName = "Company1", KRS = "000065465", NIP = "23312655", REGON = "79823132", CompanyAddress = address };

            mockCompanyService.Setup(m => m.FindByIdAsync(It.IsAny<int>())).ReturnsAsync(company);

            controller = new CompanyManagementController(mockCompanyService.Object, mapper);

            var result = await controller.Delete(1) as ViewResult;
            var resultModel = (CompanyDetailsViewModel)result.Model;

            Assert.IsNotNull(result, "Result is null");
            Assert.AreEqual(1, resultModel.ComanyId);
            Assert.AreEqual("Company1", resultModel.CompanyName);
            Assert.AreEqual("TestCity", resultModel.City);
        }

        [Test]
        public async Task DeleteConfirm_removes_company_data_and_redirects_to_Index_action()
        {
            controller = new CompanyManagementController(mockCompanyService.Object, mapper);
            var result = await controller.DeleteConfirm(1) as RedirectToRouteResult;

            Assert.IsNotNull(result, "Result is null");
            Assert.AreEqual("Index", result.RouteValues["action"], "Invalid action name");
            mockCompanyService.Verify(m => m.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
        #endregion
    }
}