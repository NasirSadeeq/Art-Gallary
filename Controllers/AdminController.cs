using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Art_Gallery.Interface;
using Art_Gallery.Models;
using Art_Gallery.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Art_Gallery.Controllers;

[Authorize(Roles ="Admin")]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly Iadmin iadmin;

    public AdminController(Iadmin iadmin)
     {
        this.iadmin = iadmin;
    }

    [HttpGet("GetUser/{Email?}")]
     public async Task<ActionResult<Registration>> GetUser(string Email=null)
        {
            try
            {
                if(Email==null)
                {
                     return Ok(await iadmin.GetAllUsers());
                }
                else
                {
                     var result=await iadmin.GetUser(Email.ToLower());
                    if(result== null)
                    {
                        return NotFound($"No User is Registered with this Email found. {Email}");
                    }
                    return Ok(result);

                }
               
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
         [HttpPatch("UpdateProfile/{Email}")]
        public async Task<ActionResult<Admin>> UpdateProfile(string Email, Admin reg)
        {
            try
            {
                if (Email !=reg.Email)
                    return BadRequest("Email Id Mismatch");

                var emp = await iadmin.GetUser(Email.ToLower());
                 if (emp == null)
                 {
                    return BadRequest($"No User Found with this Email Id= {Email}");
                 }

                return await iadmin.UpdateProfile(reg);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        [HttpPost("AddUser")]
         public async Task<ActionResult<Registration>> AddUser(Registration reg)
        {
            try
            {
                if (reg == null)
                    return BadRequest();

                var emp = await iadmin.GetUser(reg.Email.ToLower());
                if(emp != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "User with same Email already exist");
                  //  ModelState.AddModelError(emp.Email, "User with same Email is already exist");
                    //return BadRequest(ModelState);
                }

                var result = await iadmin.AddUser(reg);
               // return CreatedAtAction(nameof(GetUser),new {email=result.Email},result);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    [HttpGet("GetOrder/{OrderId?}")]
     public async Task<ActionResult<Order>> GetOrder(string OrderId=null)
        {
            try
            {
                if(OrderId==null)
                {
                     return Ok(await iadmin.GetAllOders());
                }
                else
                {
                     var result=await iadmin.GetOrder(OrderId);
                    if(result== null)
                    {
                        return NotFound($"No User is Registered with this Email found. {OrderId}");
                    }
                    return Ok(result);

                }
               
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    [HttpPatch("UpdateOrderStatus/{OrderId}")]
     public async Task<ActionResult<Order>> UpdateOrderStatus(String OrderId, Order activity)
        {
            try
            {
                if (OrderId != activity.OrderId)
                    throw new Exception("Activity Id MisMatch");

                var order = await iadmin.GetOrder(OrderId);
                if (order == null)
                {
                    throw new Exception($"No order with Id= {order} found");
                    //return BadRequest($"Activity with this  {id} not found");
                }
                
                return await iadmin.UpdateOrderStatus(activity);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
}
