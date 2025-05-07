using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.DBContext;
using BackEndWeb.DataBaseService.Service.Base;

namespace BackEndWeb.DataBaseService.Service;

public class TypeEventService : BaseService< Typeevent, int>
{
    public TypeEventService(Itcollegeapp context) :  base(context) { }
    
}