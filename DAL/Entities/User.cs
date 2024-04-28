namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public record User()
    {
        public required int Id { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
