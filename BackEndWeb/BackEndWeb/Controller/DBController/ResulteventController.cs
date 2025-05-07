using BackEndWeb.Controller.DBController.BaseController;
using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWeb.Controller.DBController;

[ApiController]
[Authorize]
[Route("/api/results")]
public class ResulteventController : BaseController<ResulteventView,Resultevent,int>
{
    public ResulteventController(ResulteventService service) : base(service) {}
    
    protected override Resultevent MapToModel(ResulteventView view, int id = default)
    {
        return new Resultevent()
        {
            Resultid = id == 0 ? 0 : id,
            Result = view.Result
        };
    }
}