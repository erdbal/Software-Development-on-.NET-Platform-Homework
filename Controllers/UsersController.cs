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

        /// <summary>
        /// The caller can get all users from the database.
        /// </summary>
        /// <returns> A collections of users </returns>
        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetAsync()
        {
            
            return await _userService.GetAllUsersAsync();
        }

        /// <summary>
        /// The caller can get a specific user from the database.
        /// </summary>
        /// <param name="id"> The user's id</param>
        /// <returns> Returns the user </returns>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Get(int id)
        {
            
            return await _userService.GetUserByIdAsync(id);
        }

        /// <summary>
        /// The caller can get a specific user from the database.
        /// </summary>
        /// <param name="name"> The user's name</param>
        /// <returns> Returns the user</returns>
        // GET api/<UserController>/asd
        [HttpGet("{name}/byname")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Get(string name)
        {
            
            return await _userService.GetUserByUsernameAsync(name);
        }

        /// <summary>
        /// The caller can get all groups from the database that are related to a specific user.
        /// </summary>
        /// <param name="id"> The user's id</param>
        /// <returns></returns>
        [HttpGet("{id}/groups")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups(int id)
        {
            return await _userService.GetGroupsOfUserAsync(id);
        }
        
        /// <summary>
        /// The caller can log in to the system.
        /// </summary>
        /// <remarks> Currently the token isn't used. Actual functionality is to be added at a later date or to be removed</remarks>
        /// <param name="username"> The username of the user</param>
        /// <param name="password"> The password of the user</param>
        /// <returns>A token to be used for access</returns>
        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<string>> Login([FromHeader] string username, [FromHeader] string password)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            return _loginService.RefreshOrLogin(user, password);
        }

        /// <summary>
        /// The caller can register to the system.
        /// </summary>
        /// <param name="user"> The user's name </param>
        /// <param name="password"> The user's password </param>
        /// <returns> The created user's data </returns>
        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> Post([FromBody] User user, [FromHeader] string password)
        {
            var hashedPassword = _loginService.GetHashString(password);
            var created = await _userService.CreateUserAsync(user, password);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }


        /// <summary>
        /// The caller can update a user in the database.
        /// </summary>
        /// <param name="id"> The user's id </param>
        /// <param name="user"> The user's content </param>
        /// <returns> The updated user </returns>
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> Put(int id, [FromBody] User user)
        {
            return await _userService.UpdateUserAsync(id, user);
        }

        /// <summary>
        /// The caller can delete a user from the database.
        /// </summary>
        /// <param name="id"> The user's id </param>
        /// <returns> No content </returns>
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
