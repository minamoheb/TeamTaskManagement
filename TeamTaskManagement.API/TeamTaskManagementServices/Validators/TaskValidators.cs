using FluentValidation;
using TeamTaskManagement.Services.Dtos;

namespace TeamTaskManagement.Services.Validators
{
    public class TaskItemValidator : AbstractValidator<TaskModel>
    {
        public TaskItemValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.")
                 .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

            RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            //RuleFor(x => x.DueDate)
            //    .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Due date must be a future date.");

            RuleFor(x => x.PriorityId).NotEmpty().WithMessage("Priority is required.")
                .GreaterThan(0).WithMessage("Priority must be greater than 0"); ;
            RuleFor(x => x.StatusId).NotEmpty().WithMessage("Status is required.")
                .GreaterThan(0).WithMessage("Status must be greater than 0"); ;
        }
    }
}
