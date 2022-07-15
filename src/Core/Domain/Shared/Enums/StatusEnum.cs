using Shared.Core.Domain.Impl.Attributes;
using System.ComponentModel;

namespace Actions.Core.Domain.Shared.Enums
{
    public enum StatusEnum
    {
        [Description("Active")]
        [Color("#3855B3")]
        [Order(1)]
        Active = 1,
        [Description("Concluded")]
        [Color("#44ca42")]
        [Order(2)]
        Concluded = 2,
        [Description("Cancelled")]
        [Color("#888")]
        [Order(3)]
        Cancelled = 3
    }
}
