namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public record User()
    {
        public int Id { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();

        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
