using MongoDB.Bson;

namespace TaskManager.Infrastructure.Data.Models.DataBaseModels
{
    public class UserTask : BaseDeletableModel<ObjectId>
    {
        public ObjectId CategoryId { get; set; }

        public ObjectId StatusId { get; set; }

        public ObjectId UserId { get; set; }

        public string Description { get; set; } = null!;
    }
}
