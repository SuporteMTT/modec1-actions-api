
using System.Collections.Generic;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.ResponsePlans.Entities;
using Actions.Core.Domain.ResponsePlans.Interfaces;
using System.Linq;
using Actions.Core.Domain.Shared;

namespace Actions.Infrastructure.Data.Repositories
{
    public class ResponsePlanRepository : UnitOfWork, IResponsePlanRepository
    {
        private readonly ActionsContext _context;
        public ResponsePlanRepository(ActionsContext context) : base(context)
        { 
            _context = context;
        }

        public IEnumerable<ResponsePlanDto> GetByMetadataId(string metadataId)
        {
            return (from responsePlan in _context.Set<ResponsePlan>()
                where responsePlan.MetadataId == metadataId
                select new ResponsePlanDto
                {
                    Id = responsePlan.Id,
                    ActualEndDate = responsePlan.ActualEndDate,
                    ActualStartDate = responsePlan.ActualStartDate,
                    ClosedBy = responsePlan.ClosedBy != null ? responsePlan.ClosedBy.Name : null,
                    ClosedDate = responsePlan.ClosedDate,
                    Comments = responsePlan.Comments,
                    Cost = responsePlan.Cost,
                    CreatedDate = responsePlan.CreatedDate,
                    Description = responsePlan.ActionDescription,
                    DueDate = responsePlan.DueDate,
                    MetadataId = responsePlan.MetadataId,
                    OriginalDueDate = responsePlan.OriginalDueDate,
                    Responsible = responsePlan.Responsible != null ? new Core.Domain.Users.Dtos.UserDto
                    {
                        Id = responsePlan.Responsible.Id,
                        Name = responsePlan.Responsible.Name
                    } : null,
                    Status = responsePlan.Status.Status()
                } );
        }
    }
}
