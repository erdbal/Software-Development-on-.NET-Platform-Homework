namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public class Comment
    {
        public required int Id { get; set; }

        public required string Text { get; set; }

        public User Creator { get; set; } = null!;

        public Event Event { get; set; } = null!;
    }
}
