using BackEndWeb.Controller.Model.ViewModels;
using BackEndWeb.DataBaseService.DataBaseModel;
using BackEndWeb.DataBaseService.DBContext;
using BackEndWeb.DataBaseService.Service.Base;
using Microsoft.EntityFrameworkCore;

namespace BackEndWeb.DataBaseService.Service;

public class EventService : BaseService< Event, Guid>
{
    public EventService(Itcollegeapp context) : base(context) {}
    
}