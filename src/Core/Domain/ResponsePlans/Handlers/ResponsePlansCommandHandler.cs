using System.Linq;
using System.Threading.Tasks;
using Actions.Core.Domain.ResponsePlans.Commands;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.ResponsePlans.Entities;
using Actions.Core.Domain.ResponsePlans.Interfaces;

namespace Actions.Core.Domain.ResponsePlans.Handlers
{
    public class ResponsePlansCommandHandler
    {
        private readonly IResponsePlanRepository _repository;
        public ResponsePlansCommandHandler(IResponsePlanRepository repository)
        {
            _repository = repository;
        }

        public void Handle(ProcessListResponsePlan command)
        {
            if (command != null && command.ResponsePlanList != null)
            {
                var responsePlansDB = _repository.GetByMetadataId(command.MetadataId);
                var responsePlansIdDB = responsePlansDB.Select(plan => plan.Id);

                var listToAdd = command.ResponsePlanList
                    .Where(responsePlan => !responsePlansIdDB.Contains(responsePlan.Id))
                    .Select(responsePlan => new ResponsePlan(
                        responsePlan.Description, 
                        responsePlan.Responsible.Id, 
                        responsePlan.DueDate,
                        responsePlan.Status,
                        responsePlan.ActualStartDate,
                        responsePlan.ActualEndDate,
                        responsePlan.Cost,
                        responsePlan.Comments,
                        command.MetadataId
                    ));

                _repository.InsertRange(listToAdd);

                var responsePlansIds = command.ResponsePlanList.Select(plan => plan.Id);
                var listToUpdate = responsePlansDB
                    .Where(responsePlan => responsePlansIds.Contains(responsePlan.Id))
                    .Select(responsePlan => ToResponsePlan(responsePlan));

                foreach (var item in listToUpdate)
                {
                    var responsePlan = command.ResponsePlanList.SingleOrDefault(plan => plan.Id == item.Id);

                    if (item.HasModified(responsePlan.Description, responsePlan.Responsible.Id, responsePlan.DueDate, responsePlan.OriginalDueDate, responsePlan.Status, responsePlan.ActualStartDate, responsePlan.ActualEndDate, responsePlan.Cost, responsePlan.Comments))
                        _repository.Update(item);
                }

                var listToDelete = responsePlansDB
                    .Where(responsePlan => !responsePlansIds.Contains(responsePlan.Id))
                    .Select(responsePlan => ToResponsePlan(responsePlan));

                _repository.DeleteRange(listToDelete);
            }
        }

        private ResponsePlan ToResponsePlan(ResponsePlanDto responsePlan)
        {
            return new ResponsePlan(
                responsePlan.Id,
                responsePlan.Description, 
                responsePlan.Responsible.Id, 
                responsePlan.DueDate,
                responsePlan.OriginalDueDate,
                responsePlan.Status,
                responsePlan.ActualStartDate,
                responsePlan.ActualEndDate,
                responsePlan.Cost,
                responsePlan.Comments,
                responsePlan.MetadataId,
                responsePlan.CreatedDate
            );
        }
    }

}