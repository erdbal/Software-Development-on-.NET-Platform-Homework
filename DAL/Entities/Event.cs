namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public class Event
    {
        public required int Id { get; set; }

        public required string Eventname { get; set; }

        public required string Description { get; set; }

        public required DateTime Date { get; set; }

        public required string Location { get; set; }

        public Group Group { get; set; } = null!;

        public User Creator { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
