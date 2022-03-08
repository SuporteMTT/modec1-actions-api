using System.ComponentModel;

namespace Actions.Core.Domain.Actions.Enums
{
    public enum ActionStatusEnum
    {
        [Description("Not Initiated")]
        NotInitiated = 1,
        [Description("On Going")]
        OnGoing = 2,
        [Description("Concluded")]
        Concluded = 3,
        [Description("Delayed")]
        Delayed = 4
    }
}
