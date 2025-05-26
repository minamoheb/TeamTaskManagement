using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TeamTaskManagement.Infrastructure;
using TeamTaskManagement.Infrastructure.UnitOfWork;
using TeamTaskManagement.Services.Dtos;
using TeamTaskManagement.Services.Helper;
using TeamTaskManagement.Services.Helper.Mapper;

namespace TeamTaskManagement.Services.Services
{

    public class GenericeBaseService<TModel, TEntity> : IGenericeBaseService<TModel, TEntity>
    where TModel : class
    where TEntity : class, new()
    {
        protected readonly IValidator<TModel> _validator;
        private readonly IUnitOfWork<TeamTaskServicesDBContext> _uow;

        public GenericeBaseService(IValidator<TModel> validator, IUnitOfWork<TeamTaskServicesDBContext> uow)
        {
            _validator = validator;
            _uow = uow;
        }

        public virtual async Task<ResponseBase<StatusResult>> InsertAsync(TModel model)
        {
            var result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new ValidationException(string.Join("; ", errors));
            }

            var entity = AppMapper.Mapper.Map<TEntity>(model);
            await _uow.GetRepository<TEntity>().Insert(entity);
            var success = await _uow.DoWork();

            return new ResponseBase<StatusResult>
            {
                Status = success > 0 ? StatusResult.Ok : StatusResult.BadRequest
            };
        }

        public virtual async Task<ResponseBase<StatusResult>> UpdateAsync(TModel model)
        {
            var result = await _validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new ValidationException(string.Join("; ", errors));
            }

            var entity = AppMapper.Mapper.Map<TEntity>(model);
            SetModifiedAuditFields(entity, model); // You can override this in derived class if needed

            await _uow.GetRepository<TEntity>().Update(entity);
            var success = await _uow.DoWork();

            return new ResponseBase<StatusResult>
            {
                Status = success > 0 ? StatusResult.Ok : StatusResult.BadRequest
            };
        }
        public async Task<ResponseBase<StatusResult>> DeleteAsync(long id)
        {
            var entity = await _uow.GetRepository<TEntity>().GetbyFirstordefault(x => EF.Property<long>(x, "Id") == id, null, null, false);

            if (entity == null)
            {
                return new ResponseBase<StatusResult> { Status = StatusResult.Failed };
            }

            await _uow.GetRepository<TEntity>().Delete(entity);
            var result = await _uow.DoWork();

            return result > 0
                ? new ResponseBase<StatusResult> { Status = StatusResult.Ok }
                : new ResponseBase<StatusResult> { Status = StatusResult.BadRequest };
        }

        protected virtual void SetModifiedAuditFields(TEntity entity, TModel model)
        {
            // Optional: override in derived service if needed to set ModifiedBy, ModifiedDate etc.
        }
    }


}
