using AutoMapper;
using BusinessLogicLayer;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using System.Threading.Tasks;
using System.Web.Http;
using TaskSolution.ExceptionFilters;
using TaskSolution.ViewModels;

namespace TaskSolution.Controllers
{
    [ApiExceptionFilter]
    [RoutePrefix("api/company")]
    public class CompanyApiController : ApiController
    {
        private readonly ICompanyService companyService;
        private readonly IMapper mapper;
        private readonly ISearchDetailService searchDetailService;
        public CompanyApiController(ICompanyService companyService, IMapper mapper, ISearchDetailService searchDetailService)
        {
            this.companyService = companyService;
            this.mapper = mapper;
            this.searchDetailService = searchDetailService;
        }

        [HttpGet]
        [Route("details")]
        public async Task<IHttpActionResult> GetCompanyDetails(string number ="")
        {
            if (string.IsNullOrEmpty(number))
                return BadRequest();

            NumberType numberType = CompanyNumbersManagement.RecognizeNumberType(number);

            if (numberType == NumberType.Unrecognized)
                return BadRequest();

            Company company = await companyService.GetCompanyDetailsAsync(number, numberType);

            if (company == null)
                return NotFound();
            
            SearchDetail searchDetail = new SearchDetail();
            searchDetail.ComanyId = company.ComanyId;
            searchDetail.Number = number;
            searchDetail.NumberType = numberType;
            searchDetail.HeaderValues = Request.Headers.ToString();
            await searchDetailService.CreateAsync(searchDetail);

            CompanyDetailsApiViewModel viewModel = mapper.Map<CompanyDetailsApiViewModel>(company);
            return Ok(viewModel);
        }
    }
}