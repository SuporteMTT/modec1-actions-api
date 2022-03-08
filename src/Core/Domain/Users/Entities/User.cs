using Shared.Core.Domain.Impl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Users.Entities
{
    public class User : Entity<User, string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileId { get; set; }
        public string SecondaryProfileId { get; set; }
        public byte[] Photo { get; set; }
        public bool? HasPhoto { get; set; }
    }
}
