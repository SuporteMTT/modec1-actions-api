using Actions.Core.Domain.Users.Entities;
using Shared.Core.Domain.Impl.Enum;
using System.Collections.Generic;

namespace Actions.Core.Domain.Shared.Interfaces.Entities
{
    public interface ITokenUtil
    {
        string Id { get; }
        string Name { get; }
        string IpAddress { get; }
        string Token { get; }
        ICollection<FeatureProfile> Permissions { get; }

        /// <summary>
        /// Preenche os dados do token
        /// </summary>
        /// <param name="id">Id único</param>
        /// <param name="name">Nome do usuário</param>
        /// <param name="ipAddress">IP do usuário</param>
        /// <param name="token">Token</param>
        /// <param name="permissions">Permissões <see cref="FeatureProfile"/></param>
        void FillData(string id, string name, string ipAddress, string token, ICollection<FeatureProfile> permissions);

        /// <summary>
        /// Verifica se possui permissão a funcionalidade e a ação
        /// </summary>
        /// <param name="feature"><see cref="FeatureEnum"/></param>
        /// <param name="action"><see cref="ActionEnum"/></param>
        /// <returns></returns>
        bool HasPermission(FeatureEnum feature, ActionEnum action);

        /// <summary>
        /// Verifica a permissão à funcionalidade e a ção
        /// </summary>
        /// <param name="feature"><see cref="FeatureEnum"/></param>
        /// <param name="action"><see cref="ActionEnum"/></param>
        /// <returns>true se possuri a permissão ou <see cref="System.UnauthorizedAccessException"/></returns>
        bool TryGetPermissionAndThrow(FeatureEnum feature, ActionEnum action);
    }
}
