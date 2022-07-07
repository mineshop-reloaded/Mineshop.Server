using Mineshop.Server.Model.Models;

namespace Mineshop.Server.Service.Services.Interfaces;

public interface IMineshopService<T> where T : MineshopModel
{
    Task<T?> GetByIdentifier(Guid identifier);
    Task<List<T>> GetAll();
    Task<T> Create(T model);
    Task<T> Update(T model);
    Task<T?> Delete(Guid identifier);
}