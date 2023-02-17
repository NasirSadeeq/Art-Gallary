using Art_Gallery.Models;
using Art_Gallery.DTO;

namespace Art_Gallery.Interface;

public interface Iexhibation
{
     Task<ExhibationDTO> CreateExhibation(Exhibation exhibation);
     Task<Exhibation> UpdateExhibation(Exhibation userDetails);
     Task DeleteExhibation(string ExhibationId);
      Task<ExhibationDTO> GetExhibation(string ExhibationId);
      Task<Art> GetArt(string ArtId);
      Task<IEnumerable<Art>> GetAllArt();
      Task<GallaryRepresentative> UpdateProfile(GallaryRepresentative Details);
}