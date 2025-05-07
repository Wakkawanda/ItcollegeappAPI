using BackEndWeb.DataBaseService.Service.Base;
using Microsoft.AspNetCore.Mvc;

namespace BackEndWeb.Controller.DBController.BaseController;

public abstract class BaseController<TView, TModel, TId> : ControllerBase where TModel : class
{
    protected readonly IBaseService<TModel, TId> Service;
    
    public BaseController(IBaseService<TModel,TId> service)
    {
        Service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TView request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var model = MapToModel(request);
        
        var (response, message) = await Service.Add(model);
        
        return response == null
            ? BadRequest(message) 
            : Ok(new { response, message });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(TId id)
    {
        if (id is int intId && intId < 0)
        {
            return BadRequest("The ID cannot be less than zero");
        }
        
        var (model, message) = await Service.Delete(id);
        
        return model == null 
            ? NotFound(message) 
            : Ok(message);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(TId id)
    {
        if (id is int intId && intId < 0)
        {
            return BadRequest("The ID cannot be less than zero");
        }

        var (model, message) = await Service.Get(id);

        return model == null
            ? NotFound(message) 
            : Ok(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var (model, message) = await Service.GetAll();

        return model == null
            ? NotFound(message) 
            : Ok(model);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update([FromBody]TView request, TId id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        } 

        var model = MapToModel(request, id);
        var (response, message) = await Service.Update(id, model);
        
        return response == null 
            ? NotFound(message) 
            : Ok(new { response, message });
    }
    
    
    protected abstract TModel MapToModel(TView view, TId? id = default);
}