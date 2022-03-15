using MongoDB.Driver;
using System;

namespace Challenge.Domain.Interfaces
{
    public interface ICensusContext : IDisposable
    {
        IMongoDatabase _dataBase { get; }
    }
}
