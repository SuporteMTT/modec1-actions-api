using System.ComponentModel;
using Shared.Core.Domain.Impl.Attributes;

namespace Actions.Core.Domain.StatusHistories.Enums
{
    public enum StatusHistoryEnum
    {
        [Description("Active")]
        [Color("#3855B3")]
        Active = 1,
        [Description("Concluded")]
        [Color("#44ca42")]
        Concluded = 2,
        [Description("Cancelled")]
        [Color("#888")]
        Cancelled = 3,

        [Description("Not Initiated")]
        [Color("#a3a5a5")]
        NotInitiated = 11,
        [Description("On Going")]
        [Color("#486b00")]
        OnGoing = 12,
        [Color("#c00000")]
        Delayed = 13,
    }
}