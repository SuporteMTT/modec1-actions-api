namespace Actions.Core.Domain.Actions.Queries
{
    public class GetActionByMetadataIdQuery
    {
        public string MetadataId { get; set; }

        public GetActionByMetadataIdQuery(string metadataId)
        {
            MetadataId = metadataId;
        }
    }
}
