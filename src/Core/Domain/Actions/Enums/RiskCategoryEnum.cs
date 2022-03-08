using System.ComponentModel;

namespace Actions.Core.Domain.Actions.Enums
{
    public enum RiskCategoryEnum
    {
        [Description("1.1 Dangerous Activity")]
        DangerousActivity = 1,
        [Description("1.2 Performance")]
        Performance = 2,
        [Description("1.3 Documentation")]
        Documentation = 3,
        [Description("1.4 Innovation")]
        Innovation = 4,
        [Description("1.5 Defects / Failures")]
        DefectsFailures = 5,
        [Description("1.6 Logistics")]
        Logistics = 6,
        [Description("1.7 Design")]
        Design = 7,
        [Description("1.8 Technology")]
        Technology = 8,
        [Description("2.1 Integration Management")]
        IntegrationManagement = 9,
        [Description("2.2 Scope Management")]
        ScopeManagement = 10,
        [Description("2.3 Schedule Management")]
        ScheduleManagement = 11,
        [Description("2.4 Cost Management")]
        CostManagement = 12,
        [Description("2.5 Quality Management")]
        QualityManagement = 13,
        [Description("2.6 Communications Management")]
        CommunicationsManagement = 14,
        [Description("2.7 Resource Management")]
        ResourceManagement = 15,
        [Description("2.8 Procurement & Supply Management")]
        ProcurementSupplyManagement = 16,
        [Description("2.9 Stakeholders Management")]
        StakeholdersManagement = 17,
        [Description("3.1 Organizational Culture")]
        OrganizationalCulture = 18,
        [Description("3.2 Strategy")]
        Strategy = 19,
        [Description("3.3 Organizational Structure")]
        OrganizationalStructure = 19,
        [Description("3.4 Finances")]
        Finances = 20,
        [Description("3.5 Other Projects")]
        OtherProjects = 21,
        [Description("3.6 Internal Processes")]
        InternalProcesses = 22,
        [Description("4.1 Customs")]
        Customs = 23,
        [Description("4.2 Client")]
        Client = 24,
        [Description("4.3 Local Community / Society")]
        LocalCommunitySociety = 25,
        [Description("4.4 Competitors")]
        Competitors = 26,
        [Description("4.5 Environmental Conditions")]
        EnvironmentalConditions = 27,
        [Description("4.6 Economy")]
        Economy = 28,
        [Description("4.7 Public Events")]
        PublicEvents = 29,
        [Description("4.8 Suppliers")]
        Suppliers = 30,
        [Description("4.9 Laws / Regulations")]
        LawsRegulations = 31,
        [Description("4.10 Market")]
        Market = 32,
        [Description("4.11 Licensing / Regulatory Bodies")]
        LicensingRegulatoryBodies = 33,
        [Description("4.12 Partnerships")]
        Partnerships = 34,
        [Description("4.13 Politics")]
        Politics = 35,
        [Description("4.14 Labor Unions")]
        LaborUnions = 36,
        [Description("4.15 Third Parties")]
        ThirdParties = 37,
        [Description("4.16 Taxes")]
        Taxes = 38,
        [Description("4.17 Public Health")]
        PublicHealth = 39
    }
}
