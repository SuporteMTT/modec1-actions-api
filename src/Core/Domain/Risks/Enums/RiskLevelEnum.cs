using System.ComponentModel;
using Shared.Core.Domain.Impl.Attributes;

namespace Actions.Core.Domain.Shared.Enums
{
    public enum RiskLevelEnum
    {
        [Description("High")]
        [Color("#c00000")]
        High_Risk_Very_High_Impact_X_Very_High_Probability = 11,

        [Description("High")]
        [Color("#c00000")]
        High_Risk_High_Impact_X_Very_High_Probability = 21,

        [Description("High")]
        [Color("#c00000")]
        High_Risk_Medium_Impact_X_Very_High_Probability = 31,

        [Description("Moderate")]
        [Color("#ffc000")]
        Moderate_Risk_Low_Impact_X_Very_High_Probability = 41,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_Very_High_Probability = 51,


        [Description("High")]
        [Color("#c00000")]
        High_Risk_Very_High_Impact_X_High_Probability = 12,

        [Description("High")]
        [Color("#c00000")]
        High_Risk_High_Impact_X_High_Probability = 22,

        [Description("Moderate")]
        [Color("#ffc000")]
        Moderate_Risk_Medium_Impact_X_High_Probability = 32,

        [Description("Moderate")]
        [Color("#ffc000")]
        Moderate_Risk_Low_Impact_X_High_Probability = 42,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_High_Probability = 52,


        [Description("High")]
        [Color("#c00000")]
        High_Risk_Very_High_Impact_X_Medium_Probability = 13,

        [Description("High")]
        [Color("#c00000")]
        High_Risk_High_Impact_X_Medium_Probability = 23,

        [Description("Moderate")]
        [Color("#ffc000")]
        Moderate_Risk_Medium_Impact_X_Medium_Probability = 33,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Low_Impact_X_Medium_Probability = 43,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_Medium_Probability = 53,


        [Description("High")]
        [Color("#c00000")]
        High_Risk_Very_High_Impact_X_Low_Probability = 14,

        [Description("Moderate")]
        [Color("#ffc000")]
        Moderate_Risk_High_Impact_X_Low_Probability = 24,

        [Description("Moderate")]
        [Color("#ffc000")]
        Moderate_Risk_Medium_Impact_X_Low_Probability = 34,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Low_Impact_X_Low_Probability = 44,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_Low_Probability = 54,


        [Description("Moderate")]
        [Color("#ffc000")]
        Moderate_Risk_Very_High_Impact_X_Very_Low_Probability = 15,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_High_Impact_X_Very_Low_Probability = 25,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Medium_Impact_X_Very_Low_Probability = 35,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Low_Impact_X_Very_Low_Probability = 45,

        [Description("Low")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_Very_Low_Probability = 55
    }
}
