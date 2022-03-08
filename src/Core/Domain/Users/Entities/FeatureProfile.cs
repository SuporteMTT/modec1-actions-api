using Shared.Core.Domain.Impl.Entity;
using Shared.Core.Domain.Impl.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Users.Entities
{
    public class FeatureProfile : Entity<FeatureProfile, string>
    {
        public FeatureEnum Name { get; set; }
        public string ProfileId { get; set; }
        public int Actions { get; set; }
    }
}
