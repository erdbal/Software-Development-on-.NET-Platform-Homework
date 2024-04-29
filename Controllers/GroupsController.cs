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

        // GET: api/<GroupController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> Get()
        {
            return await _groupService.GetAllGroupsAsync();
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> Get(int id)
        {
            return await _groupService.GetGroupByIdAsync(id);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int id)
        {
            var users = await _groupService.GetAllUsersInGroup(id);
            return users;
        }

        [HttpPost("join/{id}")]
        public async Task<ActionResult<Group>> JoinGroup(int id, [FromHeader] int userId)
        {
            await _groupService.AddUserToGroupAsync(id, userId);
            return Created();
        }

        [HttpDelete("leave/{id}")]
        public async Task<ActionResult<Group>> LeaveGroup(int id, [FromHeader] int userId)
        {
            await _groupService.RemoveUserFromGroup(id, userId);
            return NoContent();
        }

        // POST api/<GroupController>
        [HttpPost]
        public async Task<ActionResult<Group>> Post([FromBody] Group group)
        {
            var created = await _groupService.CreateGroupAsync(group);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> Put(int id, [FromBody] Group group)
        {
            return await _groupService.UpdateGroupnameAsync(id, group);
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _groupService.DeleteGroupAsync(id);
            return NoContent();
        }
    }
}
