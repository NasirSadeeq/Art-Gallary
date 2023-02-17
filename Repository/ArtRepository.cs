
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Art_Gallery.DbContextClasses;
using Art_Gallery.Interface;
using Art_Gallery.Models;
using System.Text;

namespace Art_Gallery.Repository;
public class ArtRepository : Iartist
{
    private readonly GallaryDbContext dbContext;

    public ArtRepository(GallaryDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Art> AddArt(Art ArtDetails)
    {
        var data=new Art()
        {
            ArtistId=ArtDetails.ArtistId,
            Title=ArtDetails.Title,
            ArtType=ArtDetails.ArtType,
            Description=ArtDetails.Description,
            ArtPrice=ArtDetails.ArtPrice,
            imagePath=ArtDetails.imagePath
        };
        var result=await dbContext.arts.AddAsync(data);
       await dbContext.SaveChangesAsync();
       return result.Entity;
    }

    public async Task DeleteArt(string ArtId)
    {
         var result = await dbContext.arts.FirstOrDefaultAsync(e => e.ArtId ==ArtId);
            if (result != null)
            {
                dbContext.arts.Remove(result);
                await dbContext.SaveChangesAsync();
            }
    }

    public async Task<IEnumerable<Art>> GetAllArt()
    {
        return await dbContext.arts.ToListAsync();
    }

    public async Task<Art> GetArt(string ArtId)
    {
         return await dbContext.arts.FirstOrDefaultAsync(e => e.ArtId == ArtId);
    }

    public async Task<Art> UpdateArt(Art ArtDetails)
    {
        var result = await dbContext.arts.FirstOrDefaultAsync(e => e.ArtId == ArtDetails.ArtId);
            if (result != null)
            {
                result.Title=ArtDetails.Title;
                result.ArtType=ArtDetails.ArtType;
                result.ArtPrice=ArtDetails.ArtPrice;
                result.Description=ArtDetails.Description;
                result.imagePath=ArtDetails.imagePath;
                await dbContext.SaveChangesAsync();
                return result;
            }
            return null;
    }

     public async Task<Artist> UpdateProfile(Artist ArtDetails)
    {
        var result = await dbContext.artists.FirstOrDefaultAsync(e => e.ArtistId == ArtDetails.ArtistId);
            if (result != null)
            {
                result.FirstName=ArtDetails.FirstName;
                result.LastName=ArtDetails.LastName;
                result.MobileNo=ArtDetails.MobileNo;
                result.Address=ArtDetails.Address;
                result.Password=ArtDetails.Password;
                await dbContext.SaveChangesAsync();
                 var result2 = await dbContext.registrations.FirstOrDefaultAsync(e => e.Email == ArtDetails.Email);
                  if (result2 != null)
                        {
                            result.FirstName=ArtDetails.FirstName;
                            result.LastName=ArtDetails.LastName;
                            result.MobileNo=ArtDetails.MobileNo;
                            result.Address=ArtDetails.Address;
                            result.Password= encrtptPassword( ArtDetails.Password);
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