using Microsoft.AspNetCore.Mvc;

namespace DmcMedSync.Controller
{
    using global::DmcMedSync.DTO;
    using Microsoft.AspNetCore.Mvc;

    namespace DmcMedSync.Controller
    {
        [ApiController]
        [Route("api/user")]
        public class UserController : ControllerBase
        {
            private static readonly List<UserDTO> _user = new();
            private static int _nextId = 1;

            // GET api/user
            [HttpGet]
            public IActionResult GetAllUser()
            {
                return Ok(_user);
            }

            // GET api/user/1
            [HttpGet("{id:int}")]
            public IActionResult GetUserById(int id)
            {
                var user = _user.FirstOrDefault(u => u.Id == id);
                return user is null ? NotFound() : Ok(user);
            }

            // POST api/user
            [HttpPost]
            public IActionResult CreateUser([FromBody] UserDTO dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_user.Any(u => u.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase)))
                    return Conflict("Username already exists.");

                dto.Id = _nextId++;
                _user.Add(dto);

                return CreatedAtAction(nameof(GetUserById), new { id = dto.Id }, dto);
            }

            // PUT api/user/1
            [HttpPut("{id:int}")]
            public IActionResult UpdateUser(int id, [FromBody] UserDTO dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existing = _user.FirstOrDefault(u => u.Id == id);
                if (existing is null)
                    return NotFound();

                existing.FullName = dto.FullName;
                existing.Username = dto.Username;
                existing.Role = dto.Role;

                return Ok(existing);
            }

            // DELETE api/user/1
            [HttpDelete("{id:int}")]
            public IActionResult DeleteUser(int id)
            {
                var user = _user.FirstOrDefault(u => u.Id == id);
                if (user is null)
                    return NotFound();

                _user.Remove(user);
                return NoContent();
            }
        }
    }
}