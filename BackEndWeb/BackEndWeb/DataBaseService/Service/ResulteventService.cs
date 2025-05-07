using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.DBContext;
using BackEndWeb.DataBaseService.Service.Base;

namespace BackEndWeb.DataBaseService.Service;

public class ResulteventService : BaseService< Resultevent, int>
{
    public ResulteventService(Itcollegeapp context) : base(context) {}
    
}