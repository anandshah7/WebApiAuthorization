using Microsoft.AspNetCore.Mvc;
using WebAuth.Helpers;

namespace WebAuth;

 [Route("api/[controller]")]
[Authorize]
[ApiController]
public class HomeControllers : ControllerBase
{
    private readonly IUserInterface _userservice;

    public HomeControllers(IUserInterface userservice)
    {
        _userservice = userservice;
    }

    [HttpGet]
    public IActionResult Get(Guid id)
    {
        return Ok(_userservice.GetUserByUser_Id(id));
    }


  
}
