namespace Actions.Core.Domain.Actions.Queries
{
    public class GetDeviationAndRiskSearchQuery
    {
        public string Search { get; set; }
        public string MetadataId { get; set; }

        public GetDeviationAndRiskSearchQuery(string search, string metadataId)
        {
            Search = search;
            MetadataId = metadataId;
        }
    }
}