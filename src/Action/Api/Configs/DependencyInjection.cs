using Actions.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Actions.Api.Configs
{
    public class DependencyInjection
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<Core.Domain.Shared.Interfaces.Entities.ITokenUtil, Core.Domain.Shared.Entities.TokenUtil>();
            services.AddScoped<Core.Domain.Shared.Interfaces.IUnitOfWork, UnitOfWork>();

            services.AddScoped<Core.Domain.Users.Interfaces.IUserRepository, UserRepository>();

        }
    }
}
