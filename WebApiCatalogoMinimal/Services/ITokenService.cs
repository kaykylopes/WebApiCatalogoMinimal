using WebApiCatalogoMinimal.Models;

namespace WebApiCatalogoMinimal.Services
{
    public interface ITokenService
    {
        string GerarToken(string key, string issuer, string audience, UserModel user);
    }
}
