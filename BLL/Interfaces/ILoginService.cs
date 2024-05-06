using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface ILoginService
    {
        public string RefreshOrLogin(User user, string password);
        public int GetIdforToken(string token);
        public bool IsTokenValid(string token);
        public string GetHashString(string inputString);
    }
}
