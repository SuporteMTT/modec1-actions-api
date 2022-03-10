using Shared.Core.Domain.Impl.Attributes;
using System.ComponentModel;

namespace Actions.Core.Domain.Shared.Enums
{
    public enum StatusEnum
    {
        [Description("Active")]
        [Color("#3855B3")]
        Active = 1,
        [Description("Concluded")]
        [Color("#44ca42")]
        Concluded = 2,
        [Description("Concelled")]
        [Color("#ddd")]
        Cancelled = 3
    }
}
