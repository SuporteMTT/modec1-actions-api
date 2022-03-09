using Actions.Core.Domain.Users.Entities;
using Actions.Core.Domain.Users.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Infrasctructure.Data.Repositories
{
    public class UserRepository : UnitOfWork, IUserRepository
    {
        public UserRepository(ActionsContext context) : base(context) { }

        async public Task<ICollection<FeatureProfile>> GetPermissionsByUserAsync(string userId)
        {
            return await (from user in context.Set<User>()
                          join fea in context.Set<FeatureProfile>() on user.ProfileId equals fea.ProfileId
                          where user.Id == userId
                          select fea).AsNoTracking().ToListAsync();
        }

        async public Task<ICollection<FeatureProfile>> GetPermissionsByProfileAsync(string profileId)
        {
            return await (from fea in context.Set<FeatureProfile>()
                          where fea.ProfileId == profileId
                          select fea).AsNoTracking().ToListAsync();
        }
    }
}
