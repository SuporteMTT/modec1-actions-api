using Shared.Core.Domain.Impl.Attributes;
using System.ComponentModel;

namespace Actions.Core.Domain.Actions.Enums
{
    public enum ActionStatusEnum
    {
        [Description("Not Started")]
        [Color("#a3a5a5")]
        NotStarted = 1,
        [Color("#486b00")]
        Started = 2,
        [Color("#c00000")]
        Overdue = 3,
        [Color("#203d57")]
        Completed = 4,
    }
}
