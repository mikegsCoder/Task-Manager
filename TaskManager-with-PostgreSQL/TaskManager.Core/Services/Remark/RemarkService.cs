using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.ViewModels.Remark;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Data.Models.DataBaseModels;

namespace TaskManager.Core.Services.RemarkService
{
    public class RemarkService : IRemarkService
    {
        private readonly ApplicationDbContext db;

        public RemarkService(IServiceProvider services)
        {
            db = services.GetRequiredService<ApplicationDbContext>();
        }

        public async Task<bool> CreateRemarkAsync(string taskId, string content)
        {
            Remark remark = new Remark
            {
                Content = content,
                UserTaskId = taskId,
                CreatedOn = DateTime.Now
            };

            await db.Remarks.AddAsync(remark);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<List<RemarkViewModel>> GetRemarksAsync(string taskId)
        {
            var remarks = await db.Remarks
               .Where(x => x.UserTaskId == taskId && !x.IsDeleted)
               .OrderBy(x => x.CreatedOn)
               .Select(x => new RemarkViewModel
               {
                   Id = x.Id,
                   TaskId = x.UserTaskId,
                   Content = x.Content,
                   CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy")
               })
               .ToListAsync();
               
            return remarks;
        }

        public async Task<bool> EditRemarkAsync(string remarkId, string content)
        {
            var remark = await db.Remarks
                .Where(x => x.Id == remarkId)
                .FirstOrDefaultAsync();

            remark.Content = content;
            remark.ModifiedOn = DateTime.Now;

            db.Update(remark);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteRemarkAsync(string remarkId)
        {
            var remark = await db.Remarks
                .Where(x => x.Id == remarkId)
                .FirstOrDefaultAsync();

            remark.DeletedOn = DateTime.Now;
            remark.IsDeleted = true;

            db.Remarks.Update(remark);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
