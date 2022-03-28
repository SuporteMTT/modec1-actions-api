
using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Shared.Enums;
using Actions.Core.Domain.StatusHistories.Enums;

namespace Actions.Core.Domain.StatusHistories.Helpers
{
    public class StatusHistoryEnumHelper
    {

        public static StatusHistoryEnum ToStatusHistoryEnum(ActionStatusEnum status)
        {
            switch (status)
            {
                case ActionStatusEnum.OnGoing:
                    return StatusHistoryEnum.OnGoing;
                case ActionStatusEnum.NotInitiated:
                    return StatusHistoryEnum.NotInitiated;
                case ActionStatusEnum.Concluded:
                    return StatusHistoryEnum.Concluded;
                case ActionStatusEnum.Delayed:
                    return StatusHistoryEnum.Delayed;

                default:
                    return StatusHistoryEnum.NotInitiated;
            }
        }

        public static StatusHistoryEnum ToStatusHistoryEnum(StatusEnum status)
        {
            switch (status)
            {
                case StatusEnum.Active:
                    return StatusHistoryEnum.Active;
                case StatusEnum.Cancelled:
                    return StatusHistoryEnum.Cancelled;
                case StatusEnum.Concluded:
                    return StatusHistoryEnum.Concluded;

                default:
                    return StatusHistoryEnum.Active;
            }
        }
    }
}