using FluentValidation;

namespace Actions.Core.Domain.Deviations.Queries
{
    public class GetShortDeviationByIdQuery
    {
        public string Id { get; private set; }

        public GetShortDeviationByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetShortDeviationByIdQueryValidator : AbstractValidator<GetShortDeviationByIdQuery>
    {
        public GetShortDeviationByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    } 
}