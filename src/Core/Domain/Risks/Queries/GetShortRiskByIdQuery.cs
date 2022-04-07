using FluentValidation;

namespace Actions.Core.Domain.Risks.Queries
{
    public class GetShortRiskByIdQuery
    {
        public string Id { get; set; }

        public GetShortRiskByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetShortRiskByIdQueryValidator : AbstractValidator<GetShortRiskByIdQuery>
    {
        public GetShortRiskByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }    
}