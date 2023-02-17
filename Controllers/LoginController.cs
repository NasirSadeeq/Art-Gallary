using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Art_Gallery.Interface;
using Art_Gallery.Models;

using Microsoft.AspNetCore.Mvc;

namespace Art_Gallery.Controllers;
[ApiController]
[Route("api/[controller]")]
 public class LoginController : ControllerBase
    {
    private readonly Ilogin ilogin;

    public LoginController(Ilogin ilogin)
        {
        this.ilogin = ilogin;
        }
            [HttpPost("Login")]
            public IActionResult Login([FromBody] Login login)
            {
                var Token = ilogin.Authenticate(login.Email.ToLower(), login.Password,login.role);
                if (string.IsNullOrEmpty(Token))
                    return Unauthorized();

                return Ok(Token);
            }
        
    }
