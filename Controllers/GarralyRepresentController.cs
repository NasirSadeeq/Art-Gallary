using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Art_Gallery.Interface;
using Art_Gallery.Models;
using Art_Gallery.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Art_Gallery.Controllers;

[Authorize(Roles ="GR")]
[ApiController]
[Route("api/[controller]")]
public class GallaryRepresentController : ControllerBase
{
     private readonly Iexhibation iexhibation;
    public GallaryRepresentController(Iexhibation iexhibation)
    {
        this.iexhibation=iexhibation;
    }
     [HttpPost("AddExhibation")]
        public async Task<ActionResult<ExhibationDTO>> createExhibation(Exhibation exhibation)
        {
            try
            {
                if (exhibation == null)
                    return BadRequest();


                var result = await iexhibation.CreateExhibation(exhibation);
               // return CreatedAtAction(nameof(GetUser),new {email=result.Email},result);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("DeleteExhibation/{ExhibationId}")]
         public async Task<ActionResult> DeleteExhibation(string ExhibationId )
        {
            try
            {
                var emp = await iexhibation.GetExhibation(ExhibationId);
                if (emp == null)
                {
                    return BadRequest($"Activity with this id=  {ExhibationId} not found");
                }

                await iexhibation.DeleteExhibation(ExhibationId);
                return Ok($"Activity with this id= {ExhibationId} Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

         [HttpPut("UpdateExhibation/{ExhibationId}")]
        public async Task<ActionResult<Exhibation>> UpdateUser(string ExhibationId, Exhibation userDetails)
        {
            try
            {
                if (ExhibationId !=userDetails.ExhibationId)
                    return BadRequest("ExhibationId Mismatch");

                var emp = await iexhibation.GetExhibation(ExhibationId);
                if (emp == null)
                {
                    return BadRequest($"Exhibation with Id= {ExhibationId} not found");
                }

                return await iexhibation.UpdateExhibation(userDetails);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

         [HttpPut("UpdateProfile/{Email}")]
        public async Task<ActionResult<GallaryRepresentative>> UpdateProfile(string Email, GallaryRepresentative art)
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

                return await iexhibation.UpdateProfile(art);
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
                     return Ok(await iexhibation.GetAllArt());
                }
                else
                {
                     var result=await iexhibation.GetArt(ArtId);
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
