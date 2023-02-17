
using System.Text;
using Art_Gallery.DbContextClasses;
using Art_Gallery.Interface;
using Art_Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.Repository;
public class AdminRepository : Iadmin
{
    private readonly GallaryDbContext dbContext;

    public AdminRepository(GallaryDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<Registration> AddUser(Registration registration)
    {
        var Reg_user=new Registration()
            {
                FirstName = registration.FirstName,
                LastName=registration.LastName,
                MobileNo = registration.MobileNo, 
                Email = registration.Email.ToLower(),
                DateOfJoin=registration.DateOfJoin,
                Password = encrtptPassword( registration.Password), 
                Address = registration.Address,
                role=registration.role
            };
            var result = await dbContext.registrations.AddAsync(Reg_user);
            await dbContext.SaveChangesAsync();
            if(registration.role=="Buyer")
            {
                Buyers buyer=new Buyers(){
                    FirstName = registration.FirstName,
                LastName=registration.LastName,
                MobileNo = registration.MobileNo, 
                Email = registration.Email.ToLower(),
                DateOfJoin=registration.DateOfJoin,
                Password = encrtptPassword(registration.Password), 
                Address = registration.Address,
                };
            await dbContext.buyers.AddAsync(buyer);
            await dbContext.SaveChangesAsync();

            }
            else if(registration.role=="Artist")
            {
                Artist artist=new Artist()
                {
                    FirstName = registration.FirstName,
                    LastName=registration.LastName,
                    MobileNo = registration.MobileNo, 
                    Email = registration.Email.ToLower(),
                    DateOfJoin=registration.DateOfJoin,
                    Password = encrtptPassword(registration.Password), 
                    Address = registration.Address,
                };
            await dbContext.artists.AddAsync(artist);
            await dbContext.SaveChangesAsync();

            }
            else
            {
                GallaryRepresentative representative=new GallaryRepresentative()
                {
                    
                    FirstName = registration.FirstName,
                    LastName=registration.LastName,
                    MobileNo = registration.MobileNo, 
                    Email = registration.Email.ToLower(),
                    DateOfJoin=registration.DateOfJoin,
                    Password = encrtptPassword(registration.Password), 
                    Address = registration.Address,

                };
                 await dbContext.gallaryRepresentatives.AddAsync(representative);
            await dbContext.SaveChangesAsync();
            }
           
            return result.Entity;

    }

    public async Task DeleteUser(string Email)
    {
        var result = await dbContext.registrations.FirstOrDefaultAsync(e => e.Email ==Email.ToLower());
            if (result != null)
            {
                dbContext.registrations.Remove(result);
                await dbContext.SaveChangesAsync();
                if(result.role=="Buyer")
                {
                     var buyer = await dbContext.buyers.FirstOrDefaultAsync(e => e.Email ==Email.ToLower());
                    dbContext.buyers.Remove(buyer);
                await dbContext.SaveChangesAsync();
                }
                else if(result.role=="Artist")
                {
                     var artst = await dbContext.artists.FirstOrDefaultAsync(e => e.Email ==Email.ToLower());
                dbContext.artists.Remove(artst);
                await dbContext.SaveChangesAsync();
                var art = await dbContext.arts.FirstOrDefaultAsync(e => e.ArtistId ==artst.ArtistId);

                }
            }
    }

    public async Task<IEnumerable<Registration>> GetAllUsers()
    {
        return await dbContext.registrations.ToListAsync();
    }

    public async Task<Registration> GetUser(string Email)
    {
        return await dbContext.registrations.FirstOrDefaultAsync(x=>x.Email==Email.ToLower());
    }

     public async Task<Admin> UpdateProfile(Admin Details)
     {
          var result = await dbContext.admins.FirstOrDefaultAsync(e => e.Email == Details.Email.ToLower());
            if (result != null)
             {
                 result.FirstName=Details.FirstName;
             result.LastName=Details.LastName;
                 result.MobileNo=Details.MobileNo;
                 result.Address=Details.Address;
               // result.role=registration.role;
                var update= await dbContext.SaveChangesAsync();
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

    public async Task<IEnumerable<Order>> GetAllOders()
    {
        return await dbContext.orders.ToListAsync();
    }

    public async Task<Order> GetOrder(string OrderId)
    {
        return await dbContext.orders.FirstOrDefaultAsync(x=>x.OrderId==OrderId);
    }

    public async Task<Order> UpdateOrderStatus(Order Details)
    {
         var result = await dbContext.orders.FirstOrDefaultAsync(e => e.OrderId == Details.OrderId);
            if (result != null)
             {
                 result.Status=Details.Status;
                var update= await dbContext.SaveChangesAsync();
                 return result;
             }
             return null;
    }
}