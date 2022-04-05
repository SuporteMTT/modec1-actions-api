using System.Linq;
using System.Threading.Tasks;
using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.ResponsePlans.Commands;
using Actions.Core.Domain.ResponsePlans.Dtos;
using Actions.Core.Domain.ResponsePlans.Entities;
using Actions.Core.Domain.ResponsePlans.Interfaces;
using Actions.Core.Domain.Shared.Interfaces.Entities;

namespace Actions.Core.Domain.ResponsePlans.Handlers
{
    public class ResponsePlansCommandHandler
    {
        private readonly IResponsePlanRepository _repository;
        private readonly ITokenUtil _tokenUtil;
        public ResponsePlansCommandHandler(IResponsePlanRepository repository, ITokenUtil tokenUtil)
        {
            _repository = repository;
            _tokenUtil = tokenUtil;
        }

        public void Handle(ProcessListResponsePlan command)
        {
            if (command != null && command.ResponsePlanList != null)
            {
                var responsePlansDB = _repository.GetByMetadataId(command.MetadataId);
                bool process = true;
                if (command.ResponsePlanList.Count == 0 && (responsePlansDB == null || responsePlansDB.Count() == 0))
                    process = false;

                if (process)
                {
                    var responsePlansIdDB = responsePlansDB != null ? responsePlansDB.Select(plan => plan.Id) : Enumerable.Empty<string>();

                    var listToAdd = command.ResponsePlanList
                        .Where(responsePlan => !responsePlansIdDB.Contains(responsePlan.Id))
                        .Select(responsePlan => new ResponsePlan(
                            responsePlan.Description, 
                            responsePlan.Responsible.Id, 
                            responsePlan.DueDate,
                            (ActionStatusEnum)responsePlan.Status.Id,
                            responsePlan.ActualStartDate,
                            responsePlan.ActualEndDate,
                            responsePlan.Cost,
                            responsePlan.Comments,
                            command.MetadataId
                        ));

                    _repository.InsertRange(listToAdd);

                    if (responsePlansDB != null)
                    {
                        var responsePlansIds = command.ResponsePlanList.Select(plan => plan.Id);
                        var listToUpdate = responsePlansDB
                            .Where(responsePlan => responsePlansIds.Contains(responsePlan.Id))
                            .Select(responsePlan => ToResponsePlan(responsePlan));

                        foreach (var item in listToUpdate)
                        {
                            var responsePlan = command.ResponsePlanList.SingleOrDefault(plan => plan.Id == item.Id);

                            if (item.HasModified(responsePlan.Description, responsePlan.Responsible.Id, responsePlan.DueDate, responsePlan.OriginalDueDate, (ActionStatusEnum)responsePlan.Status.Id, responsePlan.ActualStartDate, responsePlan.ActualEndDate, responsePlan.Cost, responsePlan.Comments))
                            {
                                item.UpdateData(responsePlan.Description, responsePlan.Responsible.Id, responsePlan.DueDate, responsePlan.OriginalDueDate, (ActionStatusEnum)responsePlan.Status.Id, responsePlan.ActualStartDate, responsePlan.ActualEndDate, responsePlan.Cost, responsePlan.Comments, _tokenUtil.Id);
                                _repository.Update(item);
                            }
                        }

                        var listToDelete = responsePlansDB
                            .Where(responsePlan => !responsePlansIds.Contains(responsePlan.Id))
                            .Select(responsePlan => ToResponsePlan(responsePlan));

                        _repository.DeleteRange(listToDelete);
                    }
                }
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
                (ActionStatusEnum)responsePlan.Status.Id,
                responsePlan.ActualStartDate,
                responsePlan.ActualEndDate,
                responsePlan.Cost,
                responsePlan.Comments,
                responsePlan.MetadataId,
                responsePlan.CreatedDate.Value
            );
        }
    }

}