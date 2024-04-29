namespace Szoftverfejlesztés_dotnet_hw.BLL.Dtos
{
    public record User
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;
        
        public List<Group> Groups { get; set; } = new();

        public List<Event> CreatedEvents { get; set; } = new();

        public List<Comment> Comments { get;  set; } = new();
    }


    public record Group
    {
        public int Id { get; set; }

        public string Groupname { get; set; } = null!;

        public string Creatorname { get; set; } = null!;

        List<User> Users { get; set; } = new();

    }

    public record Event {
        public int Id { get; set; }

        public string Eventname { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Location { get; set; } = null!;

        public Group Group { get; set; } = null!;

        public User Creator { get; set; } = null!;

        public List<Comment> Comments { get; set; } = new();
    }

    public record Comment {
    
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int CreatorId { get; set; }

        public User Creator { get; set; } = null!;

        public int EventId { get; set; }

        public Event Event { get; set; } = null!;
    }
}
