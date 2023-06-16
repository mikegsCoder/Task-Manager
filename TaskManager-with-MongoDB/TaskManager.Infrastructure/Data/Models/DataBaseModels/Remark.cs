using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Infrastructure.Data.Models.DataBaseModels
{
    public class Remark : BaseDeletableModel<ObjectId>
    {
        public string Content { get; set; } = null!;

        public ObjectId TaskId { get; set; }
    }
}
