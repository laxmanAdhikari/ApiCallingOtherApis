using System.Threading.Tasks;

namespace OrderProcessing.Services
{
    public interface IAuthService
    {
        Task<string> GetToken();
    }
}
