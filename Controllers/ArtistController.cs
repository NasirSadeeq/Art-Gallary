

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Art_Gallery.Interface;
using Art_Gallery.Models;
using Art_Gallery.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Art_Gallery.Controllers;

[Authorize(Roles ="Artist")]
[ApiController]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{
     private readonly Iartist iartist;
    public ArtistController(Iartist iartist)
    {
        this.iartist=iartist;
    }
     [HttpPost("AddArtWork")]
        public async Task<ActionResult<Art>> createExhibation(Art art)
        {
            try
            {
                if (art == null)
                    return BadRequest();


                var result = await iartist.AddArt(art);
               // return CreatedAtAction(nameof(GetUser),new {email=result.Email},result);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("DeleteArtWork/{ArtId}")]
         public async Task<ActionResult> DeleteExhibation(string ArtId )
        {
            try
            {
                var emp = await iartist.GetArt(ArtId);
                if (emp == null)
                {
                    return BadRequest($"Activity with this id=  {ArtId} not found");
                }

                await iartist.DeleteArt(ArtId);
                return Ok($"Activity with this id= {ArtId} has been Deleted Successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

         [HttpPut("UpdateArtWork/{ArtId}")]
        public async Task<ActionResult<Art>> UpdateArtWork(string ArtId, Art art)
        {
            try
            {
                if (ArtId !=art.ArtId)
                    return BadRequest("Art Id Mismatch");

                var emp = await iartist.GetArt(ArtId);
                if (emp == null)
                {
                    return BadRequest($"Exhibation with Id= {ArtId} not found");
                }

                return await iartist.UpdateArt(art);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

         [HttpPut("UpdateProfile/{Email}")]
        public async Task<ActionResult<Artist>> UpdateProfile(string Email, Artist art)
        {
            try
            {
                if (Email !=art.Email)
                    return BadRequest("Art Id Mismatch");

               // var emp = await iartist.GetArt(Email);
                // if (emp == null)
                // {
                //     return BadRequest($"Exhibation with Id= {Email} not found");
                // }

                return await iartist.UpdateProfile(art);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("GetArt/{Email?}")]
         public async Task<ActionResult<Art>> GetUser(string ArtId=null)
        {
            try
            {
                if(ArtId==null)
                {
                     return Ok(await iartist.GetAllArt());
                }
                else
                {
                     var result=await iartist.GetArt(ArtId);
                    if(result== null)
                    {
                        return NotFound();
                    }
                    return Ok(result);

                }
               
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
  
}
