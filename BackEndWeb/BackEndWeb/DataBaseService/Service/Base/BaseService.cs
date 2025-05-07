using Microsoft.EntityFrameworkCore;

namespace BackEndWeb.DataBaseService.Service.Base;

// Абстрактный класс реализующий базовые операции 
public abstract class BaseService<TEntity, TId> : IBaseService<TEntity, TId> where TEntity : class
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public BaseService(DbContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public virtual async Task<(TEntity?, string?)> Add(TEntity entity)
    {
        try
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
            return (entity, "The model has been created successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null, "Error while adding model");
        }
    }
    
    public virtual async Task<(TEntity?, string?)> Get(TId id)
    {
        try
        {
            var entity = await DbSet.FindAsync(id);
            
            return entity == null 
                ? (null, "Model not found") 
                : (entity, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null, "Model not found");
        }    
    }
    
    public virtual async Task<(List<TEntity>?, string?)> GetAll()
    {
        try
        {
            var entities= await DbSet.ToListAsync();
            
            return entities.Count() == 0 
                ? (null, "Entity not found")
                : (entities, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null, "Error while getting model");
        }
    }

    public virtual async Task<(TEntity?, string?)> Delete(TId id)
    {
        try
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null) return(null, "Entity not found");

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
            
            return (null, "The model has been deleted successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null, "Error while deleting model");
        }   
    }
    
    public virtual async Task<(TEntity?, string?)> Update(TId id, TEntity entity)
    {
        try
        {
            var model = await DbSet.FindAsync(id);
            if (model == null) return(null, "Entity not found");    

            Context.Entry(model).CurrentValues.SetValues(entity);
            await Context.SaveChangesAsync();
            
            return (null, "The model has been update successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (null, "Error while deleting model");
        }   
    }
}