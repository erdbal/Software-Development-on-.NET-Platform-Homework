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

        // GET: api/<EventController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            return await _eventService.GetAllEventsAsync();
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Event>> Get(int id)
        {
            return await _eventService.GetEventByIdAsync(id);
        }

        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int id)
        {
            return await _commentService.GetCommentsByEventIdAsync(id);
        }

        [HttpPost("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Comment>> PostComment(int id, [FromBody] Comment comment)
        {
            comment.EventId = id;
            var created = await _commentService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // POST api/<EventController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Event>> Post([FromBody] Event givenevent)
        {
            var created = await _eventService.CreateEventAsync(givenevent);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Event>> Put(int id, [FromBody] Event _event)
        {
            return await _eventService.UpdateEventAsync(id, _event);
        }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
