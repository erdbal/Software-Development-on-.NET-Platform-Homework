using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.DAL;
using Szoftverfejlesztés_dotnet_hw.DAL.Entities;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Comment> CreateComment(Comment newComment)
        {
            throw new NotImplementedException();            
        }

        public Task<Comment> DeleteComment(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetCommentById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetCommentsByEventId(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetCommentsByUserID(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateComment(int id, Comment updatedComment)
        {
            throw new NotImplementedException();
        }
    }
}
