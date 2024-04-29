namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public required string Eventname { get; set; }

        public required string Description { get; set; }

        public required DateTime Date { get; set; }

        public required string Location { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; } = null!;

        public int CreatorId { get; set; }

        public User Creator { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
