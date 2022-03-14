using FluentValidation;

namespace Actions.Core.Domain.Actions.Queries
{
    public class GetActionByIdQuery
    {
        public string Id { get; set; }

        public GetActionByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetActionByIdQueryValidator : AbstractValidator<GetActionByIdQuery>
    {
        public GetActionByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }    
}