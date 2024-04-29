namespace Szoftverfejlesztés_dotnet_hw.BLL.Exceptions
{
    public class EntityByNameNotFoundException : Exception
    {
        public EntityByNameNotFoundException(string name)
        {
            Name = name;
        }

        public EntityByNameNotFoundException(string message, string name)
            : base(message)
        {
            Name = name;
        }

        public EntityByNameNotFoundException(string message, Exception innerException, string name)
            : base(message, innerException)
        {
            Name = name;
        }

        public string Name { get; }

    }
}
