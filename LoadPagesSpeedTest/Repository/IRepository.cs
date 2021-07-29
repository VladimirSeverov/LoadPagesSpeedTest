using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadPagesSpeedTest.Repository
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<int> Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity> FindById(int id);

        Task<List<TEntity>> GetEntitiesByParentId(int id); 
    }
}
