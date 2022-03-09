using Actions.Core.Domain.Shared.Interfaces.Entities;
using Actions.Core.Domain.Users.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Actions.Api.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenUtil tokenUtil)
        {
            var path = context.Request.Path.Value;
            var authorization = context.Request.Headers["Authorization"].ToString();
            var token = authorization?.Replace("Bearer ", "")?.Trim();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var usuarioService = context.RequestServices.GetService(typeof(IUserRepository)) as IUserRepository;

                var data = DecodeToken(token);
                tokenUtil.FillData(
                    data.Id,
                    data.Nome,
                    context.Connection.RemoteIpAddress.ToString(),
                    token,
                    await usuarioService.GetPermissionsByUserAsync(data.Id));
            }
            await _next(context);
        }

        private static UsuarioTokenViewModel DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);
            var usuarioDataStr = decodedToken.Payload.Claims.SingleOrDefault(x => x.Type == "udata").Value;

            return JsonSerializer.Deserialize<UsuarioTokenViewModel>(usuarioDataStr, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }

    public class UsuarioTokenViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
