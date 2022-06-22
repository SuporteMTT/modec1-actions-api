
using Shared.Core.Domain.Impl.Attributes;
using System.ComponentModel;

namespace Actions.Core.Domain.Risks.Enums
{
    public enum ProjectStepEnum
    {
        [Description("Initiation")]
        [Order(1)]
        Initiation = 1,
        [Description("Planning")]
        [Order(2)]
        Planning = 2,
        [Description("Monitoring & Control")]
        [Order(3)]
        MonitoringControl = 3
    }
}
