using TeamTaskManagement.Services.Dtos;

namespace TeamTaskManagement.Services.Services
{
    public interface ITaskService
    {
        Task<ResponseBase<IEnumerable<TaskModel>>> GetAll();
        Task<ResponseBase<TaskModel>> GetById(long id);
        Task<IEnumerable<TaskModel>> FilterData(int? statusId, string assignto);

    }
}
