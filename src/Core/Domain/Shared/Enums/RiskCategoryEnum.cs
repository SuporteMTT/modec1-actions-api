using Shared.Core.Domain.Impl.Attributes;
using System.ComponentModel;

namespace Actions.Core.Domain.Shared.Enums
{
    public enum RiskCategoryEnum
    {
        [Description("1.1 Dangerous Activity")]
        [Order(11)]
        DangerousActivity = 1,
        [Description("1.2 Performance")]
        [Order(12)]
        Performance = 2,
        [Description("1.3 Documentation")]
        [Order(13)]
        Documentation = 3,
        [Description("1.4 Innovation")]
        [Order(14)]
        Innovation = 4,
        [Description("1.5 Defects / Failures")]
        [Order(15)]
        DefectsFailures = 5,
        [Description("1.6 Logistics")]
        [Order(16)]
        Logistics = 6,
        [Description("1.7 Design")]
        [Order(17)]
        Design = 7,
        [Description("1.8 Technology")]
        [Order(18)]
        Technology = 8,
        [Description("2.1 Integration Management")]
        [Order(21)]
        IntegrationManagement = 9,
        [Description("2.2 Scope Management")]
        [Order(22)]
        ScopeManagement = 10,
        [Description("2.3 Schedule Management")]
        [Order(23)]
        ScheduleManagement = 11,
        [Description("2.4 Cost Management")]
        [Order(24)]
        CostManagement = 12,
        [Description("2.5 Quality Management")]
        [Order(25)]
        QualityManagement = 13,
        [Description("2.6 Communications Management")]
        [Order(26)]
        CommunicationsManagement = 14,
        [Description("2.7 Resource Management")]
        [Order(27)]
        ResourceManagement = 15,
        [Description("2.8 Procurement & Supply Management")]
        [Order(28)]
        ProcurementSupplyManagement = 16,
        [Description("2.9 Stakeholders Management")]
        [Order(29)]
        StakeholdersManagement = 17,
        [Description("3.1 Organizational Culture")]
        [Order(31)]
        OrganizationalCulture = 18,
        [Description("3.2 Strategy")]
        [Order(32)]
        Strategy = 19,
        [Description("3.3 Organizational Structure")]
        [Order(33)]
        OrganizationalStructure = 20,
        [Description("3.4 Finances")]
        [Order(34)]
        Finances = 21,
        [Description("3.5 Other Projects")]
        [Order(35)]
        OtherProjects = 22,
        [Description("3.6 Internal Processes")]
        [Order(36)]
        InternalProcesses = 23,
        [Description("4.1 Customs")]
        [Order(41)]
        Customs = 24,
        [Description("4.2 Client")]
        [Order(42)]
        Client = 25,
        [Description("4.3 Local Community / Society")]
        [Order(43)]
        LocalCommunitySociety = 26,
        [Description("4.4 Competitors")]
        [Order(44)]
        Competitors = 27,
        [Description("4.5 Environmental Conditions")]
        [Order(45)]
        EnvironmentalConditions = 28,
        [Description("4.6 Economy")]
        [Order(46)]
        Economy = 29,
        [Description("4.7 Public Events")]
        [Order(47)]
        PublicEvents = 30,
        [Description("4.8 Suppliers")]
        [Order(48)]
        Suppliers = 31,
        [Description("4.9 Laws / Regulations")]
        [Order(49)]
        LawsRegulations = 32,
        [Description("4.10 Market")]
        [Order(410)]
        Market = 33,
        [Description("4.11 Licensing / Regulatory Bodies")]
        [Order(411)]
        LicensingRegulatoryBodies = 34,
        [Description("4.12 Partnerships")]
        [Order(412)]
        Partnerships = 35,
        [Description("4.13 Politics")]
        [Order(413)]
        Politics = 36,
        [Description("4.14 Labor Unions")]
        [Order(414)]
        LaborUnions = 37,
        [Description("4.15 Third Parties")]
        [Order(415)]
        ThirdParties = 38,
        [Description("4.16 Taxes")]
        [Order(416)]
        Taxes = 39,
        [Description("4.17 Public Health")]
        [Order(417)]
        PublicHealth = 40
    }
}
