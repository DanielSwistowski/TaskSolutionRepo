using AutoMapper;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskSolution.AutoMapperProfiles;
using TaskSolution.Controllers;
using TaskSolution.ViewModels;

namespace TaskSolution.Tests.Controllers
{
    [TestFixture]
    public class SearchDetailControllerTest
    {
        SearchDetailController controller;
        Mock<ISearchDetailService> mockSearchDetailService;
        IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            mockSearchDetailService = new Mock<ISearchDetailService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SearchDetailProfile());
            });
            mapper = config.CreateMapper();
        }

        [Test]
        public async Task GetCompanySerachDetails_returns_BadRequest_if_companyId_is_not_provided()
        {
            controller = new SearchDetailController(mockSearchDetailService.Object, mapper);

            var result = await controller.GetCompanySerachDetails() as HttpStatusCodeResult;

            Assert.That(result, Is.Not.Null, "Action result is null");
            Assert.That(result.StatusCode, Is.EqualTo(400), "Invalid status code");
        }

        [Test]
        public async Task GetCompanySerachDetails_returns_correct_details_data()
        {
            int companyId = 1;
            List<SearchDetail> searchDetails = new List<SearchDetail>();
            searchDetails.Add(new SearchDetail { SearchDetailId = 1, ComanyId = 1, HeaderValues = "values1", Number = "9087908", NumberType = NumberType.NIP });
            searchDetails.Add(new SearchDetail { SearchDetailId = 2, ComanyId = 1, HeaderValues = "values1", Number = "4569713", NumberType = NumberType.REGON });
            searchDetails.Add(new SearchDetail { SearchDetailId = 3, ComanyId = 2, HeaderValues = "values1", Number = "7213265", NumberType = NumberType.KRS });
            mockSearchDetailService.Setup(m => m.GetAllAsync(It.IsAny<Expression<Func<SearchDetail, bool>>>())).ReturnsAsync(searchDetails.Where(c => c.ComanyId == companyId));

            controller = new SearchDetailController(mockSearchDetailService.Object, mapper);

            var result = await controller.GetCompanySerachDetails(companyId) as ViewResult;
            var resultModel = (List<SearchDetailViewModel>)result.Model;

            Assert.That(result, Is.Not.Null, "Action result is null");
            Assert.AreEqual(2, resultModel.Count(), "Invalid result count");
            Assert.AreEqual("4569713", resultModel[1].Number);
            Assert.AreEqual(NumberType.NIP, resultModel[0].NumberType);
        }
    }
}