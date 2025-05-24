using TeamTaskManagement.Services.Dtos;
using TeamTaskManagement.Services.Helper;

namespace TeamTaskManagement.Services.Services
{
    public interface ITaskService
    {
        Task<ResponseBase<StatusResult>> Insert(TaskModel model);
        Task<ResponseBase<StatusResult>> Edit(TaskModel model);
        Task Delete(long id);
        Task<ResponseBase<IEnumerable<TaskModel>>> GetAll();
        Task<ResponseBase<TaskModel>> GetById(long id);
        Task<IEnumerable<TaskModel>> FilterData(int? statusId, string assignto);

    }
}
