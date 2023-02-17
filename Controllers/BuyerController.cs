using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Art_Gallery.Interface;
using Art_Gallery.Models;
using Art_Gallery.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Art_Gallery.Controllers;

[Authorize(Roles ="Buyer")]
[ApiController]
[Route("api/[controller]")]
public class BuyerController : ControllerBase
{
    private readonly Ibuyer ibuyer;

    public BuyerController(Ibuyer ibuyer)
    {
        this.ibuyer = ibuyer;
    }

    [HttpGet("GetArtist/{Email?}")]
     public async Task<ActionResult<Artist>> GetArtist(string Email=null)
        {
            try
            {
                if(Email==null)
                {
                     return Ok(await ibuyer.GetAllArtist());
                }
                else
                {
                     var result=await ibuyer.GetArtist(Email.ToLower());
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

    [HttpGet("GetArtByArtist/{ArtistId}")]
     public async Task<ActionResult<Art>> GetArtByArtist(string ArtistId)
        {
            try
            {
                var result = await ibuyer.GetArtByArtist(ArtistId);
                if (result == null)
                {
                    throw new Exception($"No Art with  Artist id= {ArtistId} found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
     [HttpGet("GetArtByType/{Type}")]
     public async Task<ActionResult<Art>> GetArtByType(string Type)
        {
            try
            {
                var result = await ibuyer.GetArtByType(Type);
                if (result == null)
                {
                    throw new Exception($"No Art with {Type} type found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

     [HttpGet("GetExhibationDetails/{ExhibationId}")]
     public async Task<ActionResult<Exhibation>> GetExhibationDetails(string ExhibationId)
        {
            try
            {
                var result = await ibuyer.GetExhibation(ExhibationId);
                if (result == null)
                {
                    throw new Exception($"No Details again this Exhibation found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

 [HttpGet("GetExhibation")]
     public async Task<ActionResult<Exhibation>> GetExhibation()
        {
            try
            {
                var result = await ibuyer.GetAllExhibation();
                if (result == null)
                {
                    throw new Exception($"No record found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    [HttpPost("AddToCart")]
     public async Task<ActionResult<Cart>> AddToCart(Cart cart)
        {
            try
            {
                if (cart == null)
                    return BadRequest();


                var result = await ibuyer.AddToCart(cart);
                // return CreatedAtAction(nameof(GetUser),new {email=result.Email},result);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    [HttpPatch("UpdateCart/{ArtId}")]
     public async Task<ActionResult<Cart>> UpdateCart(string ArtId, Cart cart)
        {
            try
            {
                if (ArtId != cart.ArtId)
                    throw new Exception("Activity Id MisMatch");

                var emp = await ibuyer.GetCartItem(ArtId);
                if (emp == null)
                {
                   // throw new Exception($"Activity with Id= {id} not found");
                    return BadRequest($"No item with this art Id  {ArtId}found");
                }
                var admin = new Admin();
                return await ibuyer.UpdateCart(cart);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    
    [HttpDelete("DeleteItem/{ArtId}")]
        public async Task<ActionResult> DeleteItem(string ArtId)
        {
            try
            {
                var emp = await ibuyer.GetCartItem(ArtId);
                if (emp == null)
                {
                    throw new Exception($"Cart Item with Id= {ArtId} not found");
                   // return BadRequest($"User with Email {Email} not found");
                }

                 await ibuyer.DeleteItem(ArtId);
                return Ok($"Item with id= {ArtId} Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    
    [HttpGet("GetOrder/{OrderId}")]
    public async Task<ActionResult<Order>> GetOrder(string OrderId)
        {
            try
            {
                var result = await ibuyer.Getorder(OrderId);
                if (result == null)
                {
                    throw new Exception($"No Order with id {OrderId} found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


}
