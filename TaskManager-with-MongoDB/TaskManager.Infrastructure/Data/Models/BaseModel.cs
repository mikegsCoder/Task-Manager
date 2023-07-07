using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager.Infrastructure.Data.Models
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        [BsonId]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsFinished { get; set; }

        public DateTime? FinishedOn { get; set; }
    }
}
