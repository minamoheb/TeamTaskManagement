using TeamTaskManagement.Services.Dtos;
using TeamTaskManagement.Services.Helper;

namespace TeamTaskManagement.Services.Services
{

    public interface IGenericeBaseService<TModel, TEntity>
    {
        Task<ResponseBase<StatusResult>> InsertAsync(TModel model);
        Task<ResponseBase<StatusResult>> UpdateAsync(TModel model);
        Task<ResponseBase<StatusResult>> DeleteAsync(long id);
    }


}
