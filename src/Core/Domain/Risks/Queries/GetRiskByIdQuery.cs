using FluentValidation;

namespace Actions.Core.Domain.Risks.Queries
{
    public class GetRiskByIdQuery
    {
        public string Id { get; set; }

        public GetRiskByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetRiskByIdQueryValidator : AbstractValidator<GetRiskByIdQuery>
    {
        public GetRiskByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }    
}