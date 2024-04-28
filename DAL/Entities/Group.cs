namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public class Group
    {
        public required int Id { get; set; }

        public required string Groupname { get; set; }

        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
