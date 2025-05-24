using TeamTaskManagement.Core.Entities;
using TeamTaskManagement.Infrastructure;
using TeamTaskManagement.Infrastructure.UnitOfWork;
using TeamTaskManagement.Services.Dtos;
using TeamTaskManagement.Services.Helper.Mapper;

namespace TeamTaskManagement.Services.Services
{
    public class LookupItemsServices : ILookupItemsService
    {
        #region Fields
        private readonly IUnitOfWork<TeamTaskServicesDBContext> _uow;
        #endregion

        #region Ctor
        public LookupItemsServices(IUnitOfWork<TeamTaskServicesDBContext> uow)
        {
            _uow = uow;
        }
        #endregion
        public void Dispose()
        {

        }

        public async Task<ResponseBase<IEnumerable<LookupItemsModel>>> GetAllByLookupId(long lookId)
        {
            ResponseBase<IEnumerable<LookupItemsModel>> retVal = new ResponseBase<IEnumerable<LookupItemsModel>>();
            var data = await _uow.GetRepository<LookupItems>().GetList(c => c.LookupId == lookId, c => c.OrderByDescending(c => c.NameEn), null, true);
            if (data.Count == 0)
                return null;
            var mappedData = AppMapper.Mapper.Map<IEnumerable<LookupItemsModel>>(data);
            retVal = new ResponseBase<IEnumerable<LookupItemsModel>>() { Result = mappedData };
            return retVal;
        }


    }
}
