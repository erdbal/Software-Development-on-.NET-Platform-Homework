namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public required string Groupname { get; set; }

        public required string Creatorname { get; set; }   

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
