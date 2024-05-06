using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Szoftverfejlesztés_dotnet_hw.BLL.Exceptions;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;
using Szoftverfejlesztés_dotnet_hw.DAL;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;

namespace Szoftverfejlesztés_dotnet_hw.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public CommentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Comment> CreateCommentAsync(Comment newComment)
        {
            using var transaction = _context.Database.BeginTransaction();

            var efCreator = await _context.Users.SingleOrDefaultAsync(u => u.Id == newComment.CreatorId)
                ?? throw new EntityByIdNotFoundException("Creator with id not found",newComment.CreatorId);

            var efEvent = await _context.Events.SingleOrDefaultAsync(e => e.Id == newComment.EventId)
                ?? throw new EntityByIdNotFoundException("Event with id not found",newComment.EventId);

            var efComment = new DAL.Entities.Comment
            {
                Text = newComment.Text,
                Id = newComment.Id,
                CreatorId = newComment.CreatorId,
                EventId = newComment.EventId,
                Creator = efCreator,
                Event = efEvent                             
                
            };
            await _context.Comments.AddAsync(efComment);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return await GetCommentByIdAsync(efComment.Id);
        }

        public async Task DeleteCommentAsync(int id)
        {
            var efComment = _context.Comments.SingleOrDefault(e => e.Id == id)
                ?? throw new EntityByIdNotFoundException("Comment with id not found",id);
            _context.Comments.Remove(efComment);
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            var DtoComment = await _context.Comments
                .ProjectTo<Comment>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(e => e.Id == id)
                ?? throw new EntityByIdNotFoundException("Comment with id not found",id);

            return DtoComment;
        }

        public async Task<List<Comment>> GetCommentsByEventIdAsync(int eventId)
        {
            var comments = await _context.Comments
                .Include(c => c.Creator)
                .Include(c => c.Event)
                .Where(c => c.EventId == eventId)
                .ProjectTo<Comment>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return comments;
        }

        public async Task<List<Comment>> GetCommentsByUserIDAsync(int userId)
        {
            var comments = await _context.Comments
                .Where(c => c.CreatorId == userId)
                .ProjectTo<Comment>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return comments;
        }

        public async Task<Comment> UpdateCommentAsync(int id, Comment updatedComment)
        {
            using var transaction = _context.Database.BeginTransaction();

            var efComment = await _context.Comments.SingleOrDefaultAsync(e => e.Id == id)
                ?? throw new EntityByIdNotFoundException("Comment with id not found",id);

            efComment.Text = updatedComment.Text;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return await GetCommentByIdAsync(id);
        }
    }
}
