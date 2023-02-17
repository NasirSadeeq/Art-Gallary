

using System.Text;
using Art_Gallery.DbContextClasses;
using Art_Gallery.DTO;
using Art_Gallery.Interface;
using Art_Gallery.Models;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.Repository;
public class BuyerRepository : Ibuyer
{
    private readonly GallaryDbContext dbContext;

    public BuyerRepository(GallaryDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Cart> AddToCart(Cart cart)
    {
       var AddToCart=new Cart()
       {
        BuyerId=cart.BuyerId,
        ArtId=cart.ArtId,
        ArtName=cart.ArtName,
        Quantity=cart.Quantity,
        UniteCast=cart.UniteCast,
        SubTotal=(cart.Quantity*cart.UniteCast)
       };
       await dbContext.carts.AddAsync(AddToCart);
      await dbContext.SaveChangesAsync();
       return AddToCart;
    }

    public async Task DeleteItem(string ArtId)
    {
        var result = await dbContext.carts.FirstOrDefaultAsync(e => e.ArtId ==ArtId);
            if (result != null)
            {
                dbContext.carts.Remove(result);
                await dbContext.SaveChangesAsync();
    }
    }

    public async Task<IEnumerable<Artist>> GetAllArtist()
    {
        return await dbContext.artists.ToListAsync();
    }

    public async Task<IEnumerable<Cart>> GetAllCartItems(string BuyerId)
    {
        return await dbContext.carts.Where(x=>x.BuyerId==BuyerId).GroupBy(x=>x.BuyerId).SelectMany(g=>g).ToListAsync();
    
    }

    public async Task<IEnumerable<Exhibation>> GetAllExhibation()
    {
         return await dbContext.exhibations.ToListAsync();
    }

    public async Task<Art> GetArtByArtist(string ArtistId)
    {
         return await dbContext.arts.FirstOrDefaultAsync(x=>x.ArtistId==ArtistId);
    }

    public async Task<Art> GetArtByType(string Type)
    {
         return await dbContext.arts.FirstOrDefaultAsync(x=>x.ArtType==Type);
    }

    public async Task<Artist> GetArtist(string Email)
    {
         return await dbContext.artists.FirstOrDefaultAsync(x=>x.Email==Email.ToLower());
    }

    public async Task<Cart> GetCartItem(string ArtId)
    {
        return await dbContext.carts.FirstOrDefaultAsync(x=>x.ArtId==ArtId);
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

    public async Task<Order> Getorder(string OrderId)
    {
       return await dbContext.orders.FirstOrDefaultAsync(x=>x.OrderId==OrderId);
    }

    public async Task<Order> PlaceOrder(string buyerid)
    {
        List<Cart> itemList=await dbContext.carts.Where(x=>x.BuyerId==buyerid).GroupBy(x=>x.BuyerId).SelectMany(g=>g).ToListAsync();
        double total=0;
        foreach(var item in itemList)
        {
            total+=item.SubTotal;
        }
        var result= await dbContext.buyers.FirstOrDefaultAsync(e => e.UserId == buyerid);
        var orderdetails=new Order()
        {
            UserId=result.UserId,
            UserName=result.FirstName,
            Address=result.Address,
            MobileNo=result.MobileNo,
            Email=result.Email,
            date=DateTime.Now,
            Status="Pending",
            items=new List<Cart>(itemList),
            Grand_Total=total
 
        };
        await dbContext.orders.AddAsync(orderdetails);
        await dbContext.SaveChangesAsync();
        var recordRemove=dbContext.carts.Where(x=>x.BuyerId==buyerid);
        dbContext.carts.RemoveRange(recordRemove);
        dbContext.SaveChanges();
        return orderdetails;
    }

    public async Task RemoveFromCart(string ArtId)
    {
        var result = await dbContext.carts.FirstOrDefaultAsync(e => e.ArtId == ArtId);
            if (result != null)
            {
                dbContext.carts.Remove(result);
                await dbContext.SaveChangesAsync();
            }
    }

    public async Task<Cart> UpdateCart(Cart cart)
    {
        var result = await dbContext.carts.FirstOrDefaultAsync(e => e.ArtId == cart.ArtId);
            if (result != null)
            {
                result.Quantity=cart.Quantity;
                result.UniteCast=cart.UniteCast;
                result.SubTotal=cart.Quantity*cart.UniteCast;
                await dbContext.SaveChangesAsync();
                return result;
            }
            return null;
    }

    public async Task<Buyers> UpdateProfile(Buyers BuyerDetails)
    {
         var result = await dbContext.buyers.FirstOrDefaultAsync(e => e.UserId == BuyerDetails.UserId);
            if (result != null)
            {
                result.FirstName=BuyerDetails.FirstName;
                result.LastName=BuyerDetails.LastName;
                result.MobileNo=BuyerDetails.MobileNo;
                result.Address=BuyerDetails.Address;
                result.Password= encrtptPassword( BuyerDetails.Password);
                await dbContext.SaveChangesAsync();
                 var result2 = await dbContext.registrations.FirstOrDefaultAsync(e => e.Email == BuyerDetails.Email);
                  if (result2 != null)
                        {
                            result.FirstName=BuyerDetails.FirstName;
                            result.LastName=BuyerDetails.LastName;
                            result.MobileNo=BuyerDetails.MobileNo;
                            result.Address=BuyerDetails.Address;
                            result.Password= encrtptPassword( BuyerDetails.Password);
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