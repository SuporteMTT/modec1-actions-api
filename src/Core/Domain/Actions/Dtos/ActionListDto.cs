using Actions.Core.Domain.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Actions.Dtos
{
    public class ActionListDto
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Responsible { get; set; }
        public DateTime DueDate { get; set; }
        public StatusDTO Status { get; set; }
        public DateTime EndDate { get; set; }
    }
}
