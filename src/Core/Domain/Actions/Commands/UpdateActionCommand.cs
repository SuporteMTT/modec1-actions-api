
using System;
using Actions.Core.Domain.Actions.Enums;
using FluentValidation;

namespace Actions.Core.Domain.Actions.Commands
{
    public class UpdateActionCommand
    {
        public string Id { get; set; }
        public string RelatedId { get; set; }
        public string Description { get; set; }
        public string ResponsibleId { get; set; }
        public DateTime DueDate { get; set; }
        public ActionStatusEnum Status { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? Cost { get; set; }
        public string Comments { get; set; }

        public UpdateActionCommand(
            string id,
            string relatedId,
            string description,
            string responsibleId,
            DateTime dueDate,
            ActionStatusEnum status,
            DateTime? actualStartDate,
            DateTime? actualEndDate,
            decimal? cost,
            string comments)
        {
            Id = id;
            RelatedId = relatedId;
            Description = description;
            ResponsibleId = responsibleId;
            DueDate = dueDate;
            Status = status;
            ActualStartDate = actualStartDate;
            ActualEndDate = actualEndDate;
            Cost = cost;
            Comments = comments;
        }
    }

    public class UpdateActionCommandValidator : AbstractValidator<UpdateActionCommand>
    {
        public UpdateActionCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ResponsibleId).NotEmpty().WithMessage("RespnsibleId is required");
            RuleFor(x => x.DueDate).NotNull().WithMessage("Due Date is required");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status is required");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}