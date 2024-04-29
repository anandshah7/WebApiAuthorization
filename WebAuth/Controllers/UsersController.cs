using Microsoft.AspNetCore.Mvc;

namespace WebAuth;
[Route("api/[controller]")] 
[ApiController]
public class UsersController:ControllerBase
{
    private readonly IUserInterface _userService;

    
    public UsersController(IUserInterface userService)
    {
        _userService = userService;
    }

     [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateReq model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

}
