using AutoMapper;
using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskSolution.ViewModels;

namespace TaskSolution.Controllers
{
    [HandleError(View = "Error")]
    public class SearchDetailController : Controller
    {
        private readonly ISearchDetailService searchDetailService;
        private readonly IMapper mapper;
        public SearchDetailController(ISearchDetailService searchDetailService, IMapper mapper)
        {
            this.searchDetailService = searchDetailService;
            this.mapper = mapper;
        }

        public async Task<ActionResult> GetCompanySerachDetails(int companyId = 0)
        {
            if (companyId == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IEnumerable<SearchDetail> searchDetails = await searchDetailService.GetAllAsync(c => c.ComanyId == companyId);

            IEnumerable<SearchDetailViewModel> searchDetailsViewModel = mapper.Map<IEnumerable<SearchDetailViewModel>>(searchDetails);

            return View(searchDetailsViewModel);
        }
    }
}