using System.Threading.Tasks;

namespace prodrink.gateway.Services
{
    public interface ISecretsStorageService
    {
        string GetPostgresHost();
        string GetGoogleAuthClientId();
        string GetGoogleAuthClientSecret();
        string GetGrpcServiceHost(string serviceKey);
    }
}