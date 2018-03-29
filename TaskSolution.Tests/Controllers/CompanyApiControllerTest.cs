using AutoMapper;
using BusinessLogicLayer.Services;
using Moq;
using NUnit.Framework;
using TaskSolution.AutoMapperProfiles;
using System.Threading.Tasks;
using TaskSolution.Controllers;
using System.Web.Http.Results;
using DataAccessLayer.Models;
using System.Net.Http;
using System.Web.Mvc;
using TaskSolution.ViewModels;

namespace TaskSolution.Tests.Controllers
{
    [TestFixture]
    public class CompanyApiControllerTest
    {
        Mock<ICompanyService> mockCompanyService;
        Mock<ISearchDetailService> mockSearchDetailService;
        IMapper mapper;
        CompanyApiController controller;

        [SetUp]
        public void SetUp()
        {
            mockCompanyService = new Mock<ICompanyService>();
            mockSearchDetailService = new Mock<ISearchDetailService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CompanyProfile());
            });
            mapper = config.CreateMapper();
        }

        [Test]
        public async Task GetCompanyDetails_returns_BadRequest_if_number_is_not_provided()
        {
            controller = new CompanyApiController(mockCompanyService.Object, mapper, mockSearchDetailService.Object);

            var result = await controller.GetCompanyDetails();

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task GetCompanyDetails_returns_BadRequest_if_provided_number_type_is_Unrecognized()
        {
            controller = new CompanyApiController(mockCompanyService.Object, mapper, mockSearchDetailService.Object);

            var result = await controller.GetCompanyDetails("78979");

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task GetCompanyDetails_returns_NotFound_if_company_is_null()
        {
            Company company = null;
            mockCompanyService.Setup(m => m.GetCompanyDetailsAsync(It.IsAny<string>(), It.IsAny<NumberType>())).ReturnsAsync(company);
            controller = new CompanyApiController(mockCompanyService.Object, mapper, mockSearchDetailService.Object);

            var result = await controller.GetCompanyDetails("3943680458");

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task GetCompanyDetails_saves_search_details()
        {
            mockCompanyService.Setup(m => m.GetCompanyDetailsAsync(It.IsAny<string>(), It.IsAny<NumberType>())).ReturnsAsync(new Company());
            
            controller = new CompanyApiController(mockCompanyService.Object, mapper, mockSearchDetailService.Object);
            controller.Request = new HttpRequestMessage();

            await controller.GetCompanyDetails("3943680458");

            mockSearchDetailService.Verify(m => m.CreateAsync(It.IsAny<SearchDetail>()), Times.Once);
        }

        [Test]
        public async Task GetCompanyDetails_returns_company_details()
        {
            mockCompanyService.Setup(m => m.GetCompanyDetailsAsync(It.IsAny<string>(), It.IsAny<NumberType>())).ReturnsAsync(new Company());

            controller = new CompanyApiController(mockCompanyService.Object, mapper, mockSearchDetailService.Object);
            controller.Request = new HttpRequestMessage();

            var result = await controller.GetCompanyDetails("3943680458");

            Assert.IsNotNull(result, "Result is null");
            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<CompanyDetailsApiViewModel>>());
        }
    }
}