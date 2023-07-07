using TaskManager.Core.ViewModels.Remark;

namespace TaskManager.Core.Services.RemarkService
{
    public interface IRemarkService
    {
        Task<List<RemarkViewModel>> GetRemarksAsync(string taskId);

        Task<bool> CreateRemarkAsync(string taskId, string content);

        Task<bool> EditRemarkAsync(string remarkId, string content);

        Task<bool> DeleteRemarkAsync(string remarkId);
    }
}
