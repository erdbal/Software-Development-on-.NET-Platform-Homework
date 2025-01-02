using Microsoft.AspNetCore.Mvc;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Szoftverfejlesztés_dotnet_hw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        IEventService _eventService;
        ICommentService _commentService;

        public EventsController(IEventService eventService, ICommentService commentService)
        {
            _eventService = eventService;
            _commentService = commentService;

        }

        /// <summary>
        /// The caller can get all events from the database.
        /// </summary>
        /// <returns>An IEnumerable colection of all events in the database</returns>
        // GET: api/<EventController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            return await _eventService.GetAllEventsAsync();
        }

        /// <summary>
        /// The caller can get a specific event from the database.
        /// </summary>
        /// <param name="id"> The id of the event </param>
        /// <returns> The endpoint returns a specific event </returns>
        // GET api/<EventController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Event>> Get(int id)
        {
            return await _eventService.GetEventByIdAsync(id);
        }

        /// <summary>
        /// The caller can get all comments from the database that are related to a specific event.
        /// </summary>
        /// <param name="id"> The id of the event </param>
        /// <returns>Returns a colection of comments in an event</returns>
        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int id)
        {
            return await _commentService.GetCommentsByEventIdAsync(id);
        }

        /// <summary>
        /// The caller can post a new comment to the database that is related to a specific event.
        /// </summary>
        /// <param name="id"> The id of the event </param>
        /// <param name="comment"> The content of the comment </param>
        /// <returns> Returns the created comment </returns>
        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Comment>> PostComment(int id, [FromBody] Comment comment)
        {
            comment.EventId = id;
            var created = await _commentService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// The caller can create a new event in the database.
        /// </summary>
        /// <param name="givenevent"> The content of the created event</param>
        /// <returns> Returns the created event </returns>
        // POST api/<EventController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Event>> Post([FromBody] Event givenevent)
        {
            var created = await _eventService.CreateEventAsync(givenevent);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// The caller can update an event. 
        /// </summary>
        /// <param name="id"> The event's id </param>
        /// <param name="_event"> The updated content </param>
        /// <returns> Returns the updated event</returns>
        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Event>> Put(int id, [FromBody] Event _event)
        {
            return await _eventService.UpdateEventAsync(id, _event);
        }
        
        /// <summary>
        /// The caller can delete an event from the database.
        /// </summary>
        /// <param name="id"> The event's id to be deleted </param>
        /// <returns> No content if successful </returns>
        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
