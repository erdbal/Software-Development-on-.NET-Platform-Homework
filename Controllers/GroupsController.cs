using Microsoft.AspNetCore.Mvc;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Szoftverfejlesztés_dotnet_hw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// The caller can get all groups from the database.
        /// </summary>
        /// <returns> A collection of all groups </returns>
        // GET: api/<GroupController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Group>>> Get()
        {
            return await _groupService.GetAllGroupsAsync();
        }

        /// <summary>
        /// The caller can get a specific group from the database.
        /// </summary>
        /// <param name="id"> The group's id</param>
        /// <returns> Returns a specific group</returns>
        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Get(int id)
        {
            return await _groupService.GetGroupByIdAsync(id);
        }

        /// <summary>
        /// The caller can get all users from the database that are related to a specific group.
        /// </summary>
        /// <param name="id"> The group's id </param>
        /// <returns> A collection of groups </returns>
        [HttpGet("{id}/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int id)
        {
            var users = await _groupService.GetAllUsersInGroup(id);
            return users;
        }


        /// <summary>
        /// The caller can get all events from the database that are related to a specific group.
        /// </summary>
        /// <param name="id"> The group's id</param>
        /// <returns> A collections of events </returns>
        [HttpGet("{id}/events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents(int id)
        {
            var events = await _groupService.GetAllEventsInGroup(id);
            return events;
        }

        /// <summary>
        /// The caller can join a group.
        /// </summary>
        /// <param name="id"> The group's id</param>
        /// <param name="userId"> The user's id </param>
        /// <returns>The group that was joined</returns>
        [HttpPost("join/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> JoinGroup(int id, [FromHeader] int userId)
        {
            var group = await _groupService.AddUserToGroupAsync(id, userId);
            return CreatedAtAction(nameof(Get), new { id = group.Id }, group);
        }

        /// <summary>
        /// The caller can leave a group.
        /// </summary>
        /// <param name="id"> The group's id </param>
        /// <param name="userId"> The user's id</param>
        /// <returns> No content </returns>
        [HttpDelete("leave/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> LeaveGroup(int id, [FromHeader] int userId)
        {
            await _groupService.RemoveUserFromGroup(id, userId);
            return NoContent();
        }

        /// <summary>
        /// The caller can create a new group in the database.
        /// </summary>
        /// <param name="group"> The new group's content </param>
        /// <returns> The created group </returns>
        // POST api/<GroupController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Post([FromBody] Group group)
        {
            var created = await _groupService.CreateGroupAsync(group);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// The caller can update a group in the database.
        /// </summary>
        /// <param name="id"> The groupd to be updated </param>
        /// <param name="group"> The group's content </param>
        /// <returns> The updated group </returns>
        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Put(int id, [FromBody] Group group)
        {
            return await _groupService.UpdateGroupnameAsync(id, group);
        }

        /// <summary>
        /// The caller can delete a group from the database.
        /// </summary>
        /// <param name="id"> The group's id </param>
        /// <returns> No content</returns>
        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _groupService.DeleteGroupAsync(id);
            return NoContent();
        }
    }
}
