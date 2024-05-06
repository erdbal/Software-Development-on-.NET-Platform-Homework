using System.Runtime.Serialization;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Exceptions
{
    [Serializable]
    internal class EntityAlreadyExistsException : Exception
    {
        private string v;
        private string username;

        public EntityAlreadyExistsException()
        {
        }

        public EntityAlreadyExistsException(string? message) : base(message)
        {
        }

        public EntityAlreadyExistsException(string v, string username)
        {
            this.v = v;
            this.username = username;
        }

        public EntityAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}