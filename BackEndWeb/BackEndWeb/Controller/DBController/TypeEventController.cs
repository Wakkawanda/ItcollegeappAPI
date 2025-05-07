using BackEndWeb.Controller.DBController.BaseController;
using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWeb.Controller.DBController;

[ApiController]
[Authorize]
[Route("api/typeevents")]
//[Authorize]
public class TypeEventController : BaseController<TypeeventView, Typeevent, int>
{
    public TypeEventController(TypeEventService service) : base(service) {}

    protected override Typeevent MapToModel(TypeeventView view, int id = default)
    {
        return new Typeevent
        {
            Typeid = id == 0 ? 0 : id,
            Title = view.Title
        };
    }
}