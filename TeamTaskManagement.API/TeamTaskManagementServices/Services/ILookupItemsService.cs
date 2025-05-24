using TeamTaskManagement.Services.Dtos;

namespace TeamTaskManagement.Services.Services
{
    public interface ILookupItemsService
    {
        Task<ResponseBase<IEnumerable<LookupItemsModel>>> GetAllByLookupId(long lookId);
    }
}
