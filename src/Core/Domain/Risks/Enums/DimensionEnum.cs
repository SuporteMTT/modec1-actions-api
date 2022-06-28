
using Shared.Core.Domain.Impl.Attributes;

namespace Actions.Core.Domain.Risks.Enums
{
    public enum DimensionEnum
    {
        [Order(1)]
        Schedule = 1,
        [Order(2)]
        Cost = 2,
        [Order(3)]
        Quality = 3,
        [Order(4)]
        Safety = 4,
        [Order(5)]
        Other = 5
    }
}
