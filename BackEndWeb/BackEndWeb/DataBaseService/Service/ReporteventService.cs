using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.DBContext;
using BackEndWeb.DataBaseService.Service.Base;

namespace BackEndWeb.DataBaseService.Service;

public class ReporteventService : BaseService< Reportevent, int>
{
    public ReporteventService(Itcollegeapp context) : base(context) {}
    
}