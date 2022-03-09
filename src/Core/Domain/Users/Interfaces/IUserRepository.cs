using Actions.Core.Domain.Users.Dtos;
using Actions.Core.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Core.Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<FeatureProfile>> GetPermissionsByUserAsync(string userId);
        Task<ICollection<FeatureProfile>> GetPermissionsByProfileAsync(string profileId);
    }
}
