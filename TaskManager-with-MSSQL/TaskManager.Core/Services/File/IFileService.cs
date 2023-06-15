namespace TaskManager.Core.Services.File
{
    public interface IFileService
    {
        void ExportTasksAsync(string userId, string path, string format);
    }
}
