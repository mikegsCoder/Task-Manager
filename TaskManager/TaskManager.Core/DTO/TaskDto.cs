using Azure.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.DTO
{
    public class TaskDto
    {
        public string Category { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public bool IsFinished { get; set; }

        public string FinishedOn { get; set; } = null!;

        public RemarkDto[]? Remarks { get; set; }
    }
}
