using Challenge.Domain.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : EntityBase<TEntity>
    {
        Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> FindById(string id, CancellationToken cancellationToken = default);
        Task<bool> Update(string id, TEntity entity, CancellationToken cancellationToken = default);
    }
}
