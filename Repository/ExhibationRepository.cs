using Art_Gallery.DbContextClasses;
using Art_Gallery.Interface;
using Art_Gallery.Models;
using Art_Gallery.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
namespace Art_Gallery.Repository;

public class ExhibationRepository : Iexhibation
{
    private readonly GallaryDbContext dbContext;

    public ExhibationRepository(GallaryDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<ExhibationDTO> CreateExhibation(Exhibation exhibation)
    {
       // var exhib=await dbContext.exhibations.FirstOrDefaultAsync(e => e.ExhibationId == exhibation.ExhibationId);
        var artd=await dbContext.arts.FirstOrDefaultAsync(e => e.ArtId == exhibation.ArtId);
        var exhib=new Exhibation(){
            Name=exhibation.Name,
            date=exhibation.date,
            Venue=exhibation.Venue,
            ContactNo=exhibation.ContactNo,
            ArtId=artd.ArtId
        };

       var Exhibation=new ExhibationDTO()
       {
        Name=exhibation.Name,
        date=exhibation.date,
        Venue=exhibation.Venue,
        ContactNo=exhibation.ContactNo,
        ArtId=exhibation.ArtId,
        ArtPrice=artd.ArtPrice,
        ArtType=artd.ArtType,
        Title=artd.Title,
        Description=artd.Description,
        imagePath=artd.imagePath


       };
       var result=await dbContext.exhibations.AddAsync(exhib);
       await dbContext.SaveChangesAsync();
       return Exhibation;
    }

     public async Task<ExhibationDTO> GetExhibation(string ExhibationId)
        {
             var exhib=await dbContext.exhibations.FirstOrDefaultAsync(e => e.ExhibationId == ExhibationId);
             var artd=await dbContext.arts.FirstOrDefaultAsync(e => e.ArtId == exhib.ArtId);
             var arts= await dbContext.artists.FirstOrDefaultAsync(e => e.ArtistId == artd.ArtistId);
             var exh=new ExhibationDTO()
             {
                ExhibationId=exhib.ExhibationId,
                ArtisFname=arts.FirstName,
                ArtistLname=arts.LastName,
                ArtistMobile=arts.MobileNo,
                Email=arts.Email,
                ArtistAddress=arts.Address,
                Name=exhib.Name,
                date=exhib.date,
                Venue=exhib.Venue,
                ContactNo=exhib.ContactNo,
                ArtId=exhib.ArtId,
                ArtPrice=artd.ArtPrice,
                ArtType=artd.ArtType,
                Title=artd.Title,
                Description=artd.Description,
                imagePath=artd.imagePath

             };

            return exh;
        }

    public async Task DeleteExhibation(string ExhibationId)
    {
        var result = await dbContext.exhibations.FirstOrDefaultAsync(e => e.ExhibationId == ExhibationId);
            if (result != null)
            {
                dbContext.exhibations.Remove(result);
                await dbContext.SaveChangesAsync();
            }
    }

    public async Task<Exhibation> UpdateExhibation(Exhibation exhibation)
    {
         var result = await  dbContext.exhibations.FirstOrDefaultAsync(e => e.ExhibationId == exhibation.ExhibationId);
            if (result != null)
            {
               result.Name=exhibation.Name;
               result.Venue=exhibation.Venue;
               result.date=exhibation.date;
               result.ContactNo=exhibation.ContactNo;
                await dbContext.SaveChangesAsync();
                return result;

            }
            return null;
    }
    public async Task<IEnumerable<Art>> GetAllArt()
        {
            return await dbContext.arts.ToListAsync();
        }

     public async Task<Art> GetArt(string ArtId)
        {
            return await dbContext.arts.FirstOrDefaultAsync(e => e.ArtId ==ArtId);
        }

    public async Task<GallaryRepresentative> UpdateProfile(GallaryRepresentative Details)
    {
        var result = await dbContext.gallaryRepresentatives.FirstOrDefaultAsync(e => e.GrId == Details.GrId);
            if (result != null)
            {
                result.FirstName=Details.FirstName;
                result.LastName=Details.LastName;
                result.MobileNo=Details.MobileNo;
                result.Address=Details.Address;
                DateTime date=DateTime.Now;
                result.DateOfJoin=date.ToString();
                await dbContext.SaveChangesAsync();
                var result2 = await dbContext.registrations.FirstOrDefaultAsync(e => e.Email == Details.Email);
                  if (result2 != null)
                        {
                            result.FirstName=Details.FirstName;
                            result.LastName=Details.LastName;
                            result.MobileNo=Details.MobileNo;
                            result.Address=Details.Address;
                            result.Password= encrtptPassword( Details.Password);
                            await dbContext.SaveChangesAsync();
                         }
                return result;
            }
            return null;
    }

    public static string encrtptPassword(string password)
        {
            if(string.IsNullOrEmpty(password)){
                return null;

            }
            else
            {
                byte[] storepassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptpassword = Convert.ToBase64String(storepassword);
                return encryptpassword;

            }
            
        }
}