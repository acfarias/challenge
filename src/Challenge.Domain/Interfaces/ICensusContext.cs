using MongoDB.Driver;
using System;

namespace Challenge.Domain.Interfaces
{
    public interface ICensusContext
    {
        IMongoDatabase _dataBase { get; }
    }
}
