namespace Szoftverfejlesztés_dotnet_hw.BLL.Exceptions
{
    public class EntityByIdNotFoundException : Exception
    {
        public EntityByIdNotFoundException(int id)
        {
            Id = id;
        }

        public EntityByIdNotFoundException(string message, int id)
            : base(message)
        {
            Id = id;
        }

        public EntityByIdNotFoundException(string message, Exception innerException, int id)
            : base(message, innerException)
        {
            Id = id;
        }

        public int Id { get; }

    }
}
