using FluentValidation;

namespace Actions.Core.Domain.Actions.Commands
{
    public class DeleteActionCommand
    {
        public string Id { get; set; }

        public DeleteActionCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteActionCommandValidator : AbstractValidator<DeleteActionCommand>
    {
        public DeleteActionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}