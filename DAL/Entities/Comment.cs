namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public required string Text { get; set; }

        public int CreatorId { get; set; }

        public User Creator { get; set; } = null!;

        public int EventId { get; set; }

        public Event Event { get; set; } = null!;
    }
}
