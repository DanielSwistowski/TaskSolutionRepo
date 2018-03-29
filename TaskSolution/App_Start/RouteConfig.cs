using System.Web.Mvc;
using System.Web.Routing;

namespace TaskSolution
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CompaniesList",
                url: "firma/lista-firm",
                defaults: new { controller = "CompanyManagement", action = "Index" }
            );

            routes.MapRoute(
                name: "CompanyDetails",
                url: "firma/szczegoly-firmy/{companyId}",
                defaults: new { controller = "CompanyManagement", action = "Details", companyId= UrlParameter.Optional },
                constraints: new { companyId = @"\d+" }
            );

            routes.MapRoute(
                name: "AddCompany",
                url: "firma/dodaj-firme",
                defaults: new { controller = "CompanyManagement", action = "Create" }
            );

            routes.MapRoute(
                name: "EditCompany",
                url: "firma/edytuj-dane-firmy/{companyId}",
                defaults: new { controller = "CompanyManagement", action = "Edit", companyId = UrlParameter.Optional },
                constraints: new { companyId = @"\d+" }
            );

            routes.MapRoute(
                name: "DeleteCompany",
                url: "firma/usun-dane-firmy/{companyId}",
                defaults: new { controller = "CompanyManagement", action = "Delete", companyId = UrlParameter.Optional },
                constraints: new { companyId = @"\d+" }
            );

            routes.MapRoute(
                name: "SearchCompanyDetails",
                url: "firma/szczegoly-wyszukiwania/{companyId}",
                defaults: new { controller = "SearchDetail", action = "GetCompanySerachDetails", companyId = UrlParameter.Optional },
                constraints: new { companyId = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "CompanyManagement", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}