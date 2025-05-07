namespace BackEndWeb.DataBaseService.Service.Base;

// Интерфейс для базовых операций CRUD
public interface IBaseService<TEntity, TId> where TEntity : class
{
    Task<(TEntity?, string?)> Add(TEntity entity);
    Task<(TEntity?, string?)> Get(TId id);
    Task<(List<TEntity>?, string?)> GetAll();
    Task<(TEntity?, string?)> Delete(TId id);
    Task<(TEntity?, string?)> Update(TId id, TEntity entity);
}