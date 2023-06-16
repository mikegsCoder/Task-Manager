using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
