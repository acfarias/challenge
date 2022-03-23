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
            return new MongoClient(_settings.DataBaseConfiguration.ConnectionString)
                .GetDatabase(_settings.DataBaseConfiguration.DataBaseName);
        }
    }
}