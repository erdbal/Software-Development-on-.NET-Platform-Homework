using Microsoft.AspNetCore.Mvc;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Szoftverfejlesztés_dotnet_hw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        // GET: api/<CommentController>
        [HttpGet("byeventid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Comment>>> GetCommentsByEventId([FromHeader] int eventId)
        {
            return await _commentService.GetCommentsByEventIdAsync(eventId);
        }
        
        // GET: api/<CommentController>
        [HttpGet("byuserid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Comment>>> GetCommentsByUserId([FromHeader] int userId)
        {
            var comments = await _commentService.GetCommentsByUserIDAsync(userId);
            return comments;
        }


        // POST api/<CommentController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Comment>> Post([FromBody] Comment comment)
        {
            return await _commentService.CreateCommentAsync(comment);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Comment>> Put(int id, [FromBody] Comment comment)
        {
            return await _commentService.UpdateCommentAsync(id,comment);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
