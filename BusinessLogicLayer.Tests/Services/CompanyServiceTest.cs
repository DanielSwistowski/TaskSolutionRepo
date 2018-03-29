using BusinessLogicLayer.Services;
using BusinessLogicLayer.Tests.FakeContext;
using DataAccessLayer.Models;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tests.Services
{
    [TestFixture]
    public class CompanyServiceTest
    {
        CompanyService service;
        FakeDbContext context;

        [SetUp]
        public void SetUp()
        {
            context = new FakeDbContext();
            service = new CompanyService(context);
        }

        [Test]
        public async Task CreateAsync_adds_new_company_with_correct_nip_format()
        {
            Address address = new Address { City = "TestCity", HouseNumber = "22a", Street = "TestStreet", ZipCode = "44-444" };
            Company company = new Company { CompanyName = "TestName", KRS = "0000456578", NIP = "132465897", REGON = "45654456", CompanyAddress = address };

            await service.CreateAsync(company);

            var result = context.Companies.ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("PL132465897", result[0].NIP);
            Assert.AreEqual(company.CompanyName, result[0].CompanyName);
            Assert.AreEqual(company.CompanyAddress.City, result[0].CompanyAddress.City);
            Assert.AreEqual(company.CompanyAddress.ZipCode, result[0].CompanyAddress.ZipCode);
        }

        [Test]
        public async Task UpdateAsync_updates_company_data()
        {
            Address address = new Address { CompanyId = 1, City = "TestCity", HouseNumber = "22a", Street = "TestStreet", ZipCode = "44-444" };
            Company company = new Company { ComanyId = 1, CompanyName = "TestName", KRS = "0000456578", NIP = "132465897", REGON = "45654456", CompanyAddress = address };
            context.Companies.Add(company);

            Address updatedAddress = new Address { CompanyId = 1, City = "NewCity", HouseNumber = "33", Street = "UpdatedStreet", ZipCode = "11-111" };
            Company updatedCompany = new Company { ComanyId = 1, CompanyName = "NewName", KRS = "0000456578", NIP = "89980977", REGON = "45654456", CompanyAddress = updatedAddress };

            await service.UpdateAsync(updatedCompany);

            var result = context.Companies.ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("PL89980977", result[0].NIP);
            Assert.AreEqual(updatedCompany.CompanyName, result[0].CompanyName);
            Assert.AreEqual(updatedCompany.CompanyAddress.City, result[0].CompanyAddress.City);
            Assert.AreEqual(updatedCompany.CompanyAddress.Street, result[0].CompanyAddress.Street);
            Assert.AreEqual(updatedCompany.CompanyAddress.ZipCode, result[0].CompanyAddress.ZipCode);
        }
    }
}
