namespace TaskManager.Core.ViewModels.Remark
{
    public class RemarkViewModel
    {
        public string Id { get; set; } = null!;

        public string TaskId { get; set; } = null!;

        public string? Content { get; set; }

        public string? CreatedOn { get; set; }
    }
}
