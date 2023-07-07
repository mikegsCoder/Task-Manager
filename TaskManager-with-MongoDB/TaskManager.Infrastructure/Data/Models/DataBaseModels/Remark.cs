using MongoDB.Bson;

namespace TaskManager.Infrastructure.Data.Models.DataBaseModels
{
    public class Remark : BaseDeletableModel<ObjectId>
    {
        public string Content { get; set; } = null!;

        public ObjectId TaskId { get; set; }
    }
}
