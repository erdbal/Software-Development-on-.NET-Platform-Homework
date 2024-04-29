namespace Szoftverfejlesztés_dotnet_hw.BLL.Exceptions
{
    public class UnauthorizedException: Exception
    {
        public UnauthorizedException(string message)
            : base(message)
        {
        }
    }
}
