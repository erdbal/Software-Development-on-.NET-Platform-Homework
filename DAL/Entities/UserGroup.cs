namespace Szoftverfejlesztés_dotnet_hw.DAL.Entities
{
    public class UserGroup
    {
        public required int Id { get; set; }

        public required int UserId { get; set; }

        public required int GroupId { get; set; }

        public required bool UserIsCreatorOfGroup { get; set; }

        public User User { get; set; } = null!;

        public Group Group { get; set; } = null!;
    }
}
