using BackEndWeb.Controller.DBController.BaseController;
using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWeb.Controller.DBController;

[ApiController]
[Authorize]
[Route("/api/companys")]
public class CompanyController : BaseController<CompanyView, Company, int>
{
    public CompanyController(CompanyService service) : base(service){}

    protected override Company MapToModel(CompanyView view, int id = default)
    {
        return new Company()
        {
            Companyid = id == 0 ? 0 : id, // Устанавливам новое значение либо то котороенам передали.
            Title = view.Title
        };
    }
}