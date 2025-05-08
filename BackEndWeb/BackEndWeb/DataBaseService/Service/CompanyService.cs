using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.DBContext;
using BackEndWeb.DataBaseService.Service.Base;

namespace BackEndWeb.DataBaseService.Service;

public class CompanyService : BaseService< Company, int>
{
    public CompanyService(Itcollegeapp context) : base(context) {}
    
}