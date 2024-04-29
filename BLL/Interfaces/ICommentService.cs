using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Interfaces
{
    public interface ICommentService
    {
        public Task<Comment> GetCommentByIdAsync(int id);
        public Task<List<Comment>> GetCommentsByEventIdAsync(int eventId);
        public Task<List<Comment>> GetCommentsByUserIDAsync(int userId);
        public Task<Comment> CreateCommentAsync(Comment newComment);
        public Task<Comment> UpdateCommentAsync(int id, Comment updatedComment);
        public Task DeleteCommentAsync(int id);
    }
}
