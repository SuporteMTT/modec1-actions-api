using Shared.Core.Domain.Impl.Attributes;
using System.ComponentModel;

namespace Actions.Core.Domain.Actions.Enums
{
    public enum ActionStatusEnum
    {
        [Description("Not Initiated")]
        [Color("#a3a5a5")]
        NotInitiated = 1,
        [Description("On Going")]
        [Color("#486b00")]
        OnGoing = 2,
        [Color("#203d57")]
        Concluded = 4,
        [Color("#c00000")]
        Delayed = 3,
    }
}
