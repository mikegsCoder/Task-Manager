using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Infrastructure.Data.Models.DataBaseModels
{
    public class Status
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
