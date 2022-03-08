using Shared.Core.Domain.Impl.Attributes;

namespace Actions.Core.Domain.Actions.Enums
{
    public enum RiskLevelEnum
    {
        High_Risk_Very_High_Impact_X_Very_High_Probability = 11,

        High_Risk_High_Impact_X_Very_High_Probability = 21,

        High_Risk_Medium_Impact_X_Very_High_Probability = 31,

        Moderate_Risk_Low_Impact_X_Very_High_Probability = 41,

        Low_Risk_Very_Low_Impact_X_Very_High_Probability = 51,


        High_Risk_Very_High_Impact_X_High_Probability = 12,

        High_Risk_High_Impact_X_High_Probability = 22,

        Moderate_Risk_Medium_Impact_X_High_Probability = 32,

        Moderate_Risk_Low_Impact_X_High_Probability = 42,

        Low_Risk_Very_Low_Impact_X_High_Probability = 52,


        High_Risk_Very_High_Impact_X_Medium_Probability = 13,

        High_Risk_High_Impact_X_Medium_Probability = 23,

        Moderate_Risk_Medium_Impact_X_Medium_Probability = 33,

        Low_Risk_Low_Impact_X_Medium_Probability = 43,

        Low_Risk_Very_Low_Impact_X_Medium_Probability = 53,


        High_Risk_Very_High_Impact_X_Low_Probability = 14,

        Moderate_Risk_High_Impact_X_Low_Probability = 24,

        Moderate_Risk_Medium_Impact_X_Low_Probability = 34,

        Low_Risk_Low_Impact_X_Low_Probability = 44,

        Low_Risk_Very_Low_Impact_X_Low_Probability = 54,


        Moderate_Risk_Very_High_Impact_X_Very_Low_Probability = 15,

        Low_Risk_High_Impact_X_Very_Low_Probability = 25,

        Low_Risk_Medium_Impact_X_Very_Low_Probability = 35,

        Low_Risk_Low_Impact_X_Very_Low_Probability = 45,

        Low_Risk_Very_Low_Impact_X_Very_Low_Probability = 55
    }
}
