using Challenge.Domain.Core.Configurations;
using Challenge.Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Challenge.Infra.Data.Context
{
    public class CensusContext : ICensusContext
    {
        public IMongoDatabase _dataBase { get; }
        private readonly AppSettingsConfigurations _settings;

        public CensusContext(IOptions<AppSettingsConfigurations> settings)
        {
            _settings = settings.Value;
            _dataBase = GetMongoDatabase();
        }

        private IMongoDatabase GetMongoDatabase()
        {
            return new MongoClient(string.Format(_settings.DataBaseConfiguration.Server,
                                                 _settings.DataBaseConfiguration.User,
                                                 _settings.DataBaseConfiguration.Password,
                                                 _settings.DataBaseConfiguration.Cluster,
                                                 _settings.DataBaseConfiguration.DataBaseName))
                .GetDatabase(_settings.DataBaseConfiguration.DataBaseName);
        }

        public void Dispose()
        {
            _dataBase.Client.Cluster.Dispose();
        }
    }
}
