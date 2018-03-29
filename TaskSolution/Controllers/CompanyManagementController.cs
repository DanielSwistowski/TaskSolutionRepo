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
    public class CompanyManagementController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IMapper mapper;
        public CompanyManagementController(ICompanyService companyService, IMapper mapper)
        {
            this.companyService = companyService;
            this.mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var companies = await companyService.GetAllAsync();

            IEnumerable<CompanyViewModel> companiesViewModel = mapper.Map<IEnumerable<CompanyViewModel>>(companies);

            return View(companiesViewModel);
        }

        public async Task<ActionResult> Details(int companyId = 0)
        {
            if (companyId == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Company company = await companyService.FindByIdAsync(companyId);

            if (company == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            CompanyDetailsViewModel viewModel = mapper.Map<CompanyDetailsViewModel>(company);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddCompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Company company = mapper.Map<Company>(viewModel);
                company.CompanyAddress = mapper.Map<Address>(viewModel.CompanyAddress);

                await companyService.CreateAsync(company);

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public async Task<ActionResult> Edit(int companyId = 0)
        {
            if (companyId == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Company company = await companyService.FindByIdAsync(companyId);

            if(company == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            EditCompanyViewModel viewModel = mapper.Map<EditCompanyViewModel>(company);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Company company = mapper.Map<Company>(viewModel);

                await companyService.UpdateAsync(company);

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public async Task<ActionResult> Delete(int companyId = 0)
        {
            if (companyId == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Company company = await companyService.FindByIdAsync(companyId);

            if (company == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            CompanyDetailsViewModel viewModel = mapper.Map<CompanyDetailsViewModel>(company);

            return View(viewModel);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int companyId)
        {
            await companyService.DeleteAsync(companyId);

            return RedirectToAction("Index");
        }
    }
}