using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TeamTaskManagement.Core.Entities;
using TeamTaskManagement.Infrastructure;
using TeamTaskManagement.Infrastructure.UnitOfWork;
using TeamTaskManagement.Services.Dtos;
using TeamTaskManagement.Services.Helper;
using TeamTaskManagement.Services.Helper.Mapper;

namespace TeamTaskManagement.Services.Services
{
    public class TaskService : ITaskService
    {
        #region Fields
        private readonly IUnitOfWork<TeamTaskServicesDBContext> _uow;
        private readonly IValidator<TaskModel> _taskValidator;
        #endregion

        #region Ctor
        public TaskService(IUnitOfWork<TeamTaskServicesDBContext> uow, IValidator<TaskModel> taskValidator)
        {
            _uow = uow;
            _taskValidator = taskValidator;
        }
        #endregion
        public void Dispose()
        {

        }
        public async Task<ResponseBase<StatusResult>> Insert(TaskModel model)
        {
            ResponseBase<StatusResult> retval = new ResponseBase<StatusResult>();
            ValidationResult result = await _taskValidator.ValidateAsync(model);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new ValidationException(string.Join("; ", errors));
            }
            var mappeddata = AppMapper.Mapper.Map<Tasks>(model);
            await _uow.GetRepository<Tasks>().Insert(mappeddata);
            var accept = await _uow.DoWork();
            if (accept > 0)
                return new ResponseBase<StatusResult>() { Status = StatusResult.Ok };
            else
                return new ResponseBase<StatusResult>() { Status = StatusResult.BadRequest };
        }
        public async Task<ResponseBase<StatusResult>> Edit(TaskModel model)
        {
            ResponseBase<StatusResult> retval = new ResponseBase<StatusResult>();
            ValidationResult result = await _taskValidator.ValidateAsync(model);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
                throw new ValidationException(string.Join("; ", errors));
            }
            var mappedData = AppMapper.Mapper.Map<Tasks>(model);
            mappedData.ModifiedDate = DateTime.Now;
            var res = await _uow.GetRepository<Tasks>().Update(mappedData);
            var accept = await _uow.DoWork();
            if (accept > 0)
                return new ResponseBase<StatusResult>() { Status = StatusResult.Ok };
            else
                return new ResponseBase<StatusResult>() { Status = StatusResult.BadRequest };
        }

        public async Task Delete(long id)
        {
            var data = await _uow.GetRepository<Tasks>().GetbyFirstordefault(c => c.Id == id, null, null, false);
            if (data != null)
            {
                var mappeddata = AppMapper.Mapper.Map<Tasks>(data);
                await _uow.GetRepository<Tasks>().Delete(mappeddata);
                var accept = await _uow.DoWork();
            }
        }
        public async Task<ResponseBase<IEnumerable<TaskModel>>> GetAll()
        {
            ResponseBase<IEnumerable<TaskModel>> retVal = new ResponseBase<IEnumerable<TaskModel>>();
            var data = await _uow.GetRepository<Tasks>().GetList(null, c => c.OrderByDescending(c => c.CreatedDate), t => t
              .Include(c => c.Status)
              .Include(c => c.Priority), true);
            if (data.Count == 0)
                return null;
            var mappedData = AppMapper.Mapper.Map<IEnumerable<TaskModel>>(data);
            retVal = new ResponseBase<IEnumerable<TaskModel>>() { Result = mappedData };
            return retVal;
        }
        public async Task<ResponseBase<TaskModel>> GetById(long id)
        {
            ResponseBase<TaskModel> retVal = new ResponseBase<TaskModel>();
            var data = await _uow.GetRepository<Tasks>().GetbyFirstordefault(c => c.Id == id, c => c.OrderByDescending(c => c.CreatedDate), t => t
              .Include(c => c.Status)
              .Include(c => c.Priority), true);
            if (data == null)
                return null;

            var mappedData = AppMapper.Mapper.Map<TaskModel>(data);
            retVal = new ResponseBase<TaskModel>() { Result = mappedData };
            return retVal;
        }
        public async Task<IEnumerable<TaskModel>> FilterData(int? statusId, string assignto)
        {

            Expression<Func<Tasks, bool>> predicate = null;
            predicate = c => (c.StatusId == statusId || statusId == 0)
            && (c.AssignedTo == assignto || assignto == null);

            var data = await _uow.GetRepository<Tasks>().GetList(predicate, c => c.OrderByDescending(x => x.Id), t => t?
                         .Include(c => c.Status)
                         .Include(c => c.Priority), false);
            if (data.Count == 0)
                return null;
            var mappedData = AppMapper.Mapper.Map<IEnumerable<TaskModel>>(data);
            return mappedData;

        }


    }
}
