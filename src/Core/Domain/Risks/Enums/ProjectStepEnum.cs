
using System.ComponentModel;

namespace Actions.Core.Domain.Risks.Enums
{
    public enum ProjectStepEnum
    {
        [Description("Initiation")]
        Initiation = 1,
        [Description("Planning")]
        Planning = 2,
        [Description("Monitoring & Control")]
        MonitoringControl = 3
    }
}
