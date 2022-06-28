using Shared.Core.Domain.Impl.Attributes;
using System.ComponentModel;

namespace Actions.Core.Domain.Actions.Enums
{
    public enum ActionStatusEnum
    {
        [Description("Not Initiated")]
        [Color("#a3a5a5")]
        [Order(1)]
        NotInitiated = 1,
        [Description("On Going")]
        [Color("#486b00")]
        [Order(2)]
        OnGoing = 2,
        [Color("#44ca42")]
        [Order(3)]
        Concluded = 4,
        [Color("#c00000")]
        [Order(4)]
        Delayed = 3,
    }
}
