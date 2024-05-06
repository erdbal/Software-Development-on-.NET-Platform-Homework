using Microsoft.AspNetCore.Mvc;
using Szoftverfejlesztés_dotnet_hw.BLL.Dtos;
using Szoftverfejlesztés_dotnet_hw.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Szoftverfejlesztés_dotnet_hw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ILoginService _loginService;

        public UsersController(IUserService userService, ILoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
        }

        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetAsync()
        {
            return await _userService.GetAllUsersAsync();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> Get(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        // GET api/<UserController>/asd
        [HttpGet("{name}/byname")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> Get(string name)
        {
            return await _userService.GetUserByUsernameAsync(name);
        }

        [HttpGet("{id}/groups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups(int id)
        {
            return await _userService.GetGroupsOfUserAsync(id);
        }
        
        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Login([FromHeader] string username, [FromHeader] string password)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            return _loginService.RefreshOrLogin(user, password);
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> Post([FromBody] User user, [FromHeader] string password)
        {
            var hashedPassword = _loginService.GetHashString(password);
            var created = await _userService.CreateUserAsync(user, password);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
        {
            return await _userService.UpdateUserAsync(id, user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
