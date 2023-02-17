using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Art_Gallery.Interface;
using Art_Gallery.Models;

using Microsoft.AspNetCore.Mvc;

namespace Art_Gallery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly Iregistration registration;

    public RegistrationController(Iregistration registration)
    {
        this.registration = registration;
    }
     [HttpPost("SignUp")]
        public async Task<ActionResult<Registration>> AddUser(Registration userDetails)
        {
            try
            {
                if (userDetails == null)
                    return BadRequest();

                var emp = await  registration.Getuser(userDetails.Email.ToLower());
                if(emp != null)
                {
                    ModelState.AddModelError(emp.Email, "User with same Email is already exist");
                    return BadRequest(ModelState);
                }

                var result = await registration.registration(userDetails);
               // return CreatedAtAction(nameof(GetUser),new {email=result.Email},result);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
  
}
