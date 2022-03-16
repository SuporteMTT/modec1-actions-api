using System.ComponentModel;
using Shared.Core.Domain.Impl.Attributes;

namespace Actions.Core.Domain.Shared.Enums
{
    public enum RiskLevelEnum
    {
        [Description("High Risk | Very High Impact X Very High Probability")]
        [Color("#c00000")]
        High_Risk_Very_High_Impact_X_Very_High_Probability = 11,

        [Description("High Risk | High Impact X Very High Probability")]
        [Color("#c00000")]
        High_Risk_High_Impact_X_Very_High_Probability = 21,

        [Description("High Risk | Medium Impact X Very High Probability")]
        [Color("#c00000")]
        High_Risk_Medium_Impact_X_Very_High_Probability = 31,

        [Description("Moderate Risk | Low Impact X Very High Probability")]
        [Color("#ffc000")]
        Moderate_Risk_Low_Impact_X_Very_High_Probability = 41,

        [Description("Low Risk | Very Low Impact X Very High Probability")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_Very_High_Probability = 51,


        [Description("High Risk | Very High Impact X High Probability")]
        [Color("#c00000")]
        High_Risk_Very_High_Impact_X_High_Probability = 12,

        [Description("High Risk | High Impact X High Probability")]
        [Color("#c00000")]
        High_Risk_High_Impact_X_High_Probability = 22,

        [Description("Moderate Risk | Medium Impact X High Probability")]
        [Color("#ffc000")]
        Moderate_Risk_Medium_Impact_X_High_Probability = 32,

        [Description("Moderate Risk | Low Impact X High Probability")]
        [Color("#ffc000")]
        Moderate_Risk_Low_Impact_X_High_Probability = 42,

        [Description("Low Risk | Very Low Impact X High Probability")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_High_Probability = 52,


        [Description("High Risk | Very High Impact X Medium Probability")]
        [Color("#c00000")]
        High_Risk_Very_High_Impact_X_Medium_Probability = 13,

        [Description("High Risk | High Impact X Medium Probability")]
        [Color("#c00000")]
        High_Risk_High_Impact_X_Medium_Probability = 23,

        [Description("Moderate Risk | Medium Impact X Medium Probability")]
        [Color("#ffc000")]
        Moderate_Risk_Medium_Impact_X_Medium_Probability = 33,

        [Description("Low Risk | Low Impact X Medium Probability")]
        [Color("#486b00")]
        Low_Risk_Low_Impact_X_Medium_Probability = 43,

        [Description("Low Risk | Very Low Impact X Medium Probability")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_Medium_Probability = 53,


        [Description("High Risk | Very High Impact X Low Probability")]
        [Color("#c00000")]
        High_Risk_Very_High_Impact_X_Low_Probability = 14,

        [Description("Moderate Risk | High Impact X Low Probability")]
        [Color("#ffc000")]
        Moderate_Risk_High_Impact_X_Low_Probability = 24,

        [Description("Moderate Risk | Medium Impact X Low Probability")]
        [Color("#ffc000")]
        Moderate_Risk_Medium_Impact_X_Low_Probability = 34,

        [Description("Low Risk | Low Impact X Low Probability")]
        [Color("#486b00")]
        Low_Risk_Low_Impact_X_Low_Probability = 44,

        [Description("Low Risk | Very Low Impact X Low Probability")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_Low_Probability = 54,


        [Description("Moderate Risk | Very High Impact X Very Low Probability")]
        [Color("#ffc000")]
        Moderate_Risk_Very_High_Impact_X_Very_Low_Probability = 15,

        [Description("Low Risk | High Impact X Very Low Probability")]
        [Color("#486b00")]
        Low_Risk_High_Impact_X_Very_Low_Probability = 25,

        [Description("Low Risk | Medium Impact X Very Low Probability")]
        [Color("#486b00")]
        Low_Risk_Medium_Impact_X_Very_Low_Probability = 35,

        [Description("Low Risk | Low Impact X Very Low Probability")]
        [Color("#486b00")]
        Low_Risk_Low_Impact_X_Very_Low_Probability = 45,

        [Description("Low Risk | Very Low Impact X Very Low Probability")]
        [Color("#486b00")]
        Low_Risk_Very_Low_Impact_X_Very_Low_Probability = 55
    }
}
