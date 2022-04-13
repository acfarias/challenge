using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Challenge.Domain.Core
{
    public abstract class EntityBase<T> where T : class
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
