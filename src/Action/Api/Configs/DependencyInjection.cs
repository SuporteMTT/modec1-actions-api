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

            services.AddScoped<Core.Domain.Actions.Interfaces.IActionRepository, ActionRepository>();
            services.AddScoped<Core.Domain.Actions.Handlers.ActionsQueryHandler>();

            services.AddScoped<Core.Domain.Deviations.Interfaces.IDeviationRepository, DeviationRepository>();
            services.AddScoped<Core.Domain.Deviations.Handlers.DeviationsQueryHandler>();

            services.AddScoped<Core.Domain.Risks.Interfaces.IRiskRepository, RiskRepository>();
            services.AddScoped<Core.Domain.Risks.Handlers.RisksQueryHandler>();

            services.AddScoped<Core.Domain.Users.Interfaces.IUserRepository, UserRepository>();

        }
    }
}
