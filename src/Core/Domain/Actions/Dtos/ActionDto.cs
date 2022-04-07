using System;
using Actions.Core.Domain.Actions.Enums;
using Actions.Core.Domain.Shared.Dtos;
using Actions.Core.Domain.Users.Dtos;

namespace Actions.Core.Domain.Actions.Dtos
{
    public class ActionDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public UserDto Responsible { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? OriginalDueDate { get; set; }
        public ActionStatusEnum Status { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public ShortObjectDto Related { get; set; }    
        public string RelatedId { get; set; }    
        public decimal? Cost { get; set; }
        public string Comments { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string ClosedBy { get; set; }
    }
}
