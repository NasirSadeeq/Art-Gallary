using Art_Gallery.Interface;
using Art_Gallery.Models;
using Art_Gallery.DbContextClasses;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.Repository;

public class RegistrationRepository : Iregistration
{
    private readonly GallaryDbContext dbContext;

    public RegistrationRepository(GallaryDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

     public async Task<Registration> Getuser(string Email)
        {
            return await dbContext.registrations.FirstOrDefaultAsync(e => e.Email == Email);
        }
    public async Task<Registration> registration(Registration userDetails)
    {
       var Reg_user=new Registration()
            {
                FirstName = userDetails.FirstName,
                LastName=userDetails.LastName,
                MobileNo = userDetails.MobileNo, 
                Email = userDetails.Email.ToLower(),
                DateOfJoin=userDetails.DateOfJoin,
                Password = encrtptPassword( userDetails.Password), 
                Address = userDetails.Address,
                role=userDetails.role
            };
            var result = await dbContext.registrations.AddAsync(Reg_user);
            await dbContext.SaveChangesAsync();
            if(userDetails.role=="Buyer")
            {
                Buyers buyer=new Buyers(){
                    FirstName = userDetails.FirstName,
                LastName=userDetails.LastName,
                MobileNo = userDetails.MobileNo, 
                Email = userDetails.Email.ToLower(),
                DateOfJoin=userDetails.DateOfJoin,
                Password = encrtptPassword( userDetails.Password), 
                Address = userDetails.Address,
                };
            await dbContext.buyers.AddAsync(buyer);
            await dbContext.SaveChangesAsync();

            }
            else if(userDetails.role=="Artist")
            {
                Artist artist=new Artist()
                {
                    FirstName = userDetails.FirstName,
                    LastName=userDetails.LastName,
                    MobileNo = userDetails.MobileNo, 
                    Email = userDetails.Email.ToLower(),
                    DateOfJoin=userDetails.DateOfJoin,
                    Password = encrtptPassword( userDetails.Password), 
                    Address = userDetails.Address,
                };
            await dbContext.artists.AddAsync(artist);
            await dbContext.SaveChangesAsync();

            }
            else
            {
                GallaryRepresentative representative=new GallaryRepresentative()
                {
                    
                    FirstName = userDetails.FirstName,
                    LastName=userDetails.LastName,
                    MobileNo = userDetails.MobileNo, 
                    Email = userDetails.Email.ToLower(),
                    DateOfJoin=userDetails.DateOfJoin,
                    Password = encrtptPassword( userDetails.Password), 
                    Address = userDetails.Address,

                };
                 await dbContext.gallaryRepresentatives.AddAsync(representative);
            await dbContext.SaveChangesAsync();
            }
           
            return result.Entity;

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
