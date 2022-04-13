using Challenge.Domain.Core;
using Challenge.Domain.Interfaces;
using Challenge.Domain.Interfaces.Repository;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Infra.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase<TEntity>
    {
        private readonly IMongoCollection<TEntity> _entityCollection;

        public Repository(ICensusContext censusContext)
        {
            _entityCollection = censusContext._dataBase.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _entityCollection.InsertOneAsync(entity, null, cancellationToken);
            return entity;
        }

        public Task<TEntity> FindById(string id, CancellationToken cancellationToken = default)
        {
            return _entityCollection.Find(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> Update(string id, TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await _entityCollection.ReplaceOneAsync(e => e.Id == id, entity, (ReplaceOptions)null, cancellationToken);
            return result.IsAcknowledged;
        }
    }
}
