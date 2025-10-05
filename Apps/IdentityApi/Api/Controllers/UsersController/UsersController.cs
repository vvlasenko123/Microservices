using Api.Controllers.UsersController.Models;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UsersController;

[ApiController]
[Route("api/v1/admin/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _users;

    public UsersController(IUserService users)
    {
        _users = users;
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] CreateUserRequest req)
    {
        try
        {
            var user = _users.Create(req.FirstName, req.LastName, req.Email, req.Phone, req.Password);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var user = _users.Get(id);
        if (user == null)
        {
            return NotFound(new { message = "Пользователь не найден" });
        }
        return Ok(user);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateUserRequest req)
    {
        try
        {
            var updated = _users.Update(id, req.FirstName, req.LastName, req.Email, req.Phone);
            return Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var ok = _users.Delete(id);
        if (!ok)
        {
            return NotFound(new
                {
                    message = "Пользователь не найден"
                });
        }
        return NoContent();
    }
}