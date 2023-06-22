namespace TaskManager.Core.ViewModels.Remark
{
    public class RemarkViewModel
    {
        public string Id { get; set; } = null!;

        public string TaskId { get; set; } = null!;

        public string? Content { get; set; }

        public DateTime? CreatedOnDate { get; set; }

        public string? CreatedOn
        {
            get => CreatedOnDate == null ? "" : CreatedOnDate.Value.ToString("dd/MM/yyyy");
        }
    }
}
