using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.DBContext;
using BackEndWeb.DataBaseService.Service.Base;

namespace BackEndWeb.DataBaseService.Service;

public class EventassignmentsService : BaseService< Eventassignments, Guid>
{
    public EventassignmentsService(Itcollegeapp context) : base(context) {}
    
    
}