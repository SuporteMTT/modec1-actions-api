using System;
using Actions.Core.Domain.Shared.Dtos;

namespace Actions.Core.Domain.StatusHistories.Dtos
{
    public class StatusHistoryListDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public StatusDTO Status { get; set; }
    }
}