using Actions.Core.Domain.Shared.Interfaces.Entities;
using Actions.Core.Domain.Users.Entities;
using Shared.Core.Domain.Impl.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Shared.Entities
{
    public class TokenUtil : ITokenUtil
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string IpAddress { get; private set; }
        public string Token { get; private set; }
        public ICollection<FeatureProfile> Permissions { get; private set; }

        public void FillData(string id,
            string name,
            string ipAddress,
            string token,
            ICollection<FeatureProfile> permissions)
        {
            Id = id;
            Name = name;
            IpAddress = ipAddress;
            Token = token;
            Permissions = permissions;
        }

        public bool HasPermission(FeatureEnum feature, ActionEnum action)
        {
            var actions = Permissions.SingleOrDefault(x => x.Name == feature)?.Actions;

            return (actions & (int)action) == (int)action;
        }

        public bool TryGetPermissionAndThrow(FeatureEnum feature, ActionEnum action)
        {
            if (!HasPermission(feature, action))
                throw new UnauthorizedAccessException("You are not authorized to perform this operation");

            return true;
        }
    }
}
