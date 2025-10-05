using Api.Controllers.UserTokensController.Models;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserTokensController;

[ApiController]
[Route("api/v1/admin/tokens")]
public class UserTokensController : ControllerBase
{
    private readonly IUserTokenService _userTokenService;

    public UserTokensController(IUserTokenService userTokenService)
    {
        _userTokenService = userTokenService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTokenRequest req)
    {
        try
        {
            var userToken = _userTokenService.Create(req.UserId, req.Type, req.Value, req.ExpiresAt);
            return CreatedAtAction(nameof(GetById), new { id = userToken.Id }, userToken);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var token = _userTokenService.Get(id);

        if (token == null)
        {
            return NotFound(new
            {
                message = "Токен не найден"
            });
        }

        return Ok(token);
    }

    [HttpGet("by-user/{userId:guid}")]
    public IActionResult GetByUser([FromRoute] Guid userId)
    {
        var list = _userTokenService.GetByUser(userId);
        return Ok(list);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var result = _userTokenService.Delete(id);

        if (!result)
        {
            return NotFound(new
            {
                message = "Токен не найден"
            });
        }

        return NoContent();
    }
}