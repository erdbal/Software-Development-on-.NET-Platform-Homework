using Szoftverfejlesztés_dotnet_hw.DAL.Entities;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface ICommentService
    {
        public Task<Comment> GetCommentById(int id);
        public Task<IEnumerable<Comment>> GetCommentsByEventId(int eventId);
        public Task<IEnumerable<Comment>> GetCommentsByUserID(int userId);
        public Task<Comment> CreateComment(Comment newComment);
        public Task<Comment> UpdateComment(int id, Comment updatedComment);
        public Task<Comment> DeleteComment(int id);
    }
}
