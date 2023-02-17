using Art_Gallery.Models;
namespace Art_Gallery.Interface;
public interface Iregistration
{
     Task<Registration> registration(Registration userDetails);
     public  Task<Registration> Getuser(string Email);
}