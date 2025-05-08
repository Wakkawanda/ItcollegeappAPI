using BackEndWeb.Controller.DBController.BaseController;
using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWeb.Controller.DBController;

[ApiController]
[Authorize]
[Route("/api/reports")]
public class ReporteventController : BaseController<ReporteventView, Reportevent, int>
{
    public ReporteventController(ReporteventService service) : base(service) {}

    protected override Reportevent MapToModel(ReporteventView view, int id = default)
    {
        return new Reportevent
        {
            Reportid = id == 0 ? 0 : id, // Устанавливам новое значение либо то котороенам передали.
            Taskassignmentsid = view.Taskassignmentsid,
            Body = view.Body,
            Resultid = view.Resultid,
            Status = view.Status
        };
    }
}