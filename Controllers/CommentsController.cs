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
        /// <summary>
        /// The caller can get all comments from the database that are related to a specific event.
        /// </summary>
        /// <param name="eventId"> the id of the event the comments should belong to </param>
        /// <returns>The endpoint returns a list of Comments</returns>
        // GET: api/<CommentController>
        [HttpGet("byeventid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Comment>>> GetCommentsByEventId([FromHeader] int eventId)
        {
            return await _commentService.GetCommentsByEventIdAsync(eventId);
        }

        /// <summary>
        /// The caller can get all comments from the database that are related to a specific user.
        /// </summary>
        /// <param name="userId"> The id of the user the comments should belong to </param>
        /// <returns>The endpoint returns a list of Comments</returns>
        // GET: api/<CommentController>
        [HttpGet("byuserid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Comment>>> GetCommentsByUserId([FromHeader] int userId)
        {
            var comments = await _commentService.GetCommentsByUserIDAsync(userId);
            return comments;
        }

        /// <summary>
        /// The caller can post a new comment to the database.
        /// </summary>
        /// <param name="comment"> The comments content given in the body </param>
        /// <returns>The endpoint returns the created Comment</returns>
        // POST api/<CommentController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Comment>> Post([FromBody] Comment comment)
        {
            return await _commentService.CreateCommentAsync(comment);
        }

        /// <summary>
        /// The caller can update a comment in the database.
        /// </summary>
        /// <param name="id"> The id of the comment to be updated </param>
        /// <param name="comment"> The comments content given in the body </param>
        /// <returns>The endpoint returns the updated comment</returns>
        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Comment>> Put(int id, [FromBody] Comment comment)
        {
            return await _commentService.UpdateCommentAsync(id,comment);
        }

        /// <summary>
        /// The caller can delete a comment from the database.
        /// </summary>
        /// <param name="id"> An id of a comment </param>
        /// <returns> ActionResult containing the success in the header </returns>
        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}
