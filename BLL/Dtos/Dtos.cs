namespace Szoftverfejlesztés_dotnet_hw.BLL.Dtos
{
    public record User
    {
        public int Id { get; init; }

        public string Username { get; init; } = null!;
        
        public List<UserGroup> UserGroups { get; init; } = new();

        public List<Event> CreatedEvents { get; init; } = new();

        public List<Comment> Comments { get; init; } = new();
    }

    public record UserGroup
    {
        public int Id { get; init; }

        public int UserId { get; init; }

        public int GroupId { get; init; }

        public bool UserIsCreatorOfGroup { get; init; }

        public User User { get; init; } = null!;

        public Group Group { get; init; } = null!;
    }

    public record Group
    {
        public int Id { get; init; }

        public string Groupname { get; init; } = null!;

        public int UserGroupId { get; init; }

    }

    public record Event {
        public int Id { get; init; }

        public string Eventname { get; init; } = null!;

        public string Description { get; init; } = null!;

        public DateTime Date { get; init; }

        public string Location { get; init; } = null!;

        public Group Group { get; init; } = null!;

        public User Creator { get; init; } = null!;

        public List<Comment> Comments { get; init; } = new();
    }

    public record Comment {
    
        public int Id { get; init; }

        public string Text { get; init; } = null!;

        public User Creator { get; init; } = null!;

        public Event Event { get; init; } = null!;
    }
}
