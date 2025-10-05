using Api.Controllers.PasswordResetTokensController.Models;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.PasswordResetTokensController;

[ApiController]
[Route("api/v1/admin/password-reset-tokens")]
public class PasswordResetTokensController : ControllerBase
{
    private readonly IPasswordResetService _passwordResetService;

    public PasswordResetTokensController(IPasswordResetService passwordResetService)
    {
        _passwordResetService = passwordResetService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateResetTokenRequest req)
    {
        try
        {
            var passwordResetToken = _passwordResetService.Create(req.UserId, req.Token, req.ExpiresAt);
            return CreatedAtAction(nameof(GetById), new { id = passwordResetToken.Id }, passwordResetToken);
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
        var passwordResetToken = _passwordResetService.Get(id);

        if (passwordResetToken == null)
        {
            return NotFound(new
            {
                message = "Токен сброса пароля не найден"
            });
        }

        return Ok(passwordResetToken);
    }

    [HttpGet("by-user/{userId:guid}")]
    public IActionResult GetByUser([FromRoute] Guid userId)
    {
        var list = _passwordResetService.GetByUser(userId);
        return Ok(list);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var result = _passwordResetService.Delete(id);

        if (!result)
        {
            return NotFound(new
            {
                message = "Токен сброса пароля не найден"
            });
        }

        return NoContent();
    }
}