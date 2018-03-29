using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using System.Data.Entity;
using System;

namespace BusinessLogicLayer.Services
{
    public interface ICompanyService : IBaseService<Company>
    {
        Task<Company> GetCompanyDetailsAsync(string number, NumberType numberType);
    }

    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly ICompanyDbContext context;
        public CompanyService(ICompanyDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Company> GetCompanyDetailsAsync(string number, NumberType numberType)
        {
            switch (numberType)
            {
                case NumberType.NIP:
                    string nipNumber = number.ToCorrectlyFormatedNip();
                    return await context.Companies.Include(a => a.CompanyAddress).SingleOrDefaultAsync(n => n.NIP == nipNumber);

                case NumberType.REGON:
                    string regonNumber = number.ToOnlyDigitString();
                    return await context.Companies.Include(a => a.CompanyAddress).SingleOrDefaultAsync(n => n.REGON == regonNumber);

                case NumberType.KRS:
                    string krsNumber = number.ToOnlyDigitString();
                    return await context.Companies.Include(a => a.CompanyAddress).SingleOrDefaultAsync(n => n.KRS == krsNumber);

                default:
                    throw new ArgumentException("Invalid value", "numberType");
            }
        }

        public override Task CreateAsync(Company entity)
        {
            entity.NIP = entity.NIP.ToCorrectlyFormatedNip();
            return base.CreateAsync(entity);
        }

        public override async Task UpdateAsync(Company entity)
        {
            Company company = await context.Companies.Include("CompanyAddress").SingleAsync(c => c.ComanyId == entity.ComanyId);
            if (company != null)
            {
                company.CompanyName = entity.CompanyName;
                company.NIP = entity.NIP.ToCorrectlyFormatedNip();
                company.REGON = entity.REGON;
                company.KRS = entity.KRS;

                if (entity.CompanyAddress != null)
                {
                    Address updatedAddress = new Address();
                    updatedAddress.City = entity.CompanyAddress.City;
                    updatedAddress.Street = entity.CompanyAddress.Street;
                    updatedAddress.HouseNumber = entity.CompanyAddress.HouseNumber;
                    updatedAddress.ZipCode = entity.CompanyAddress.ZipCode;
                    company.CompanyAddress = updatedAddress;
                }
                context.SetModified(company);
                await context.SaveChangesAsync();
            }
        }
    }
}