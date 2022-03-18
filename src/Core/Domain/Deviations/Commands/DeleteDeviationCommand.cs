using FluentValidation;

namespace Actions.Core.Domain.Deviations.Commands
{
    public class DeleteDeviationCommand
    {
        public string Id { get; set; }

        public DeleteDeviationCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteDeviationCommandValidator : AbstractValidator<DeleteDeviationCommand>
    {
        public DeleteDeviationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}