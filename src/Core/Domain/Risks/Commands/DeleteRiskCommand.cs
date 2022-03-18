using FluentValidation;

namespace Actions.Core.Domain.Risks.Commands
{
    public class DeleteRiskCommand
    {
        public string Id { get; set; }

        public DeleteRiskCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteRiskCommandValidator : AbstractValidator<DeleteRiskCommand>
    {
        public DeleteRiskCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}