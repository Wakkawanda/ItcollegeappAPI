using BackEndWeb.Controller.DBController.BaseController;
using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWeb.Controller.DBController;

[ApiController]
[Authorize]
[Route("/api/events")]
public class EventController : BaseController<EventView, Event, Guid>
{
    public EventController(EventService service) : base(service){}

    protected override Event MapToModel(EventView view, Guid id = default)
    {
        return new Event()
        {
            Eventid = id == Guid.Empty ? Guid.NewGuid() : id, // Устанавливам новое значение либо то котороенам передали.
            Title = view.Title,
            Typeid = view.Typeid,
            Information = view.Information,
            Companyid = view.Companyid,
            Themeevent = view.Themeevent,
            Bodytask = view.Bodytask,
            Startdate = view.Startdate.HasValue
                ? DateTime.SpecifyKind(view.Startdate.Value, DateTimeKind.Local)
                : null,
            Enddate = view.Enddate.HasValue
                ? DateTime.SpecifyKind(view.Enddate.Value, DateTimeKind.Local)
                : null
        };
    }
}