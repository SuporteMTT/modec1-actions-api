using System;
using Actions.Core.Domain.StatusHistories.Enums;
using FluentValidation;

namespace Actions.Core.Domain.StatusHistories.Commands
{
    public class CreateStatusHistoryCommand
    {
        public CreateStatusHistoryCommand(DateTime date, string userId, StatusHistoryEnum status, string metadataId)
        {
            Date = date;
            Status = status;
            MetadataId = metadataId;
        }
        public DateTime Date { get; private set; }
        public StatusHistoryEnum Status { get; private set; }
        public string MetadataId { get; private set; }
    }

    public class CreateStatusHistoryCommandValidator : AbstractValidator<CreateStatusHistoryCommand>
    {
        public CreateStatusHistoryCommandValidator()
        {
            RuleFor(x => x.Date).NotNull().WithMessage("Date is required");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status is required");
            RuleFor(x => x.MetadataId).NotEmpty().WithMessage("MetadataId is required");
        }
    }
}