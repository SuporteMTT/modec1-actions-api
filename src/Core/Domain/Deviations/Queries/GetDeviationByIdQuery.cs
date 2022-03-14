using FluentValidation;

namespace Actions.Core.Domain.Deviations.Queries
{
    public class GetDeviationByIdQuery
    {
        public string Id { get; set; }

        public GetDeviationByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetDeviationByIdQueryValidator : AbstractValidator<GetDeviationByIdQuery>
    {
        public GetDeviationByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }    
}