using Microsoft.EntityFrameworkCore;
using Art_Gallery.Models;
namespace Art_Gallery.Interface;
public interface Iartist
{
    Task<Art> AddArt(Art ArtDetails);
    Task<Art> UpdateArt(Art ArtDetails);
    Task<Artist> UpdateProfile(Artist ArtistDetails);
    Task DeleteArt(string ArtId);
    Task <IEnumerable<Art>> GetAllArt();
    Task<Art>GetArt(string ArtId);
}